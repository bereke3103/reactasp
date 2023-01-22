import { useState } from "react";
// import Constants from "./utilities/Constants";
import PostCreateFrom from "./Components/PostCreateFrom";
import PostUpdateFrom from './Components/PostUpdateFrom';

export default function App() {

  const [posts, setPosts] = useState([]);
  const [showingCreateNewPostForm, setShowingCreateNewPostForm] = useState(false)
  const [postCurrentlyBeingUpdated, setPostCurrentlyBeingUpdated] = useState(null)

  function getPosts() {
    const url = "https://localhost:7282/get-all-post";

    fetch(url, {
      method: 'GET',
    })
      .then((response) => response.json())
      .then((postsFromServer) => {
        console.log(postsFromServer);
        setPosts(postsFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  function deletePost(postId) {
    const url = `https://localhost:7282/delete-post-id/${postId}`
    
    fetch(url, {
      method: 'DELETE'
    }).then(response=> response.json())
    .then(responseFromServer=> {
      console.log(responseFromServer);
      onPostDeleted(postId)
    }).catch((error)=> {
      console.log(error);
      alert(error)
    })
  }


  return (
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">

          {(showingCreateNewPostForm === false && postCurrentlyBeingUpdated === null) && (
            <div>
            <h1>ASP NET</h1>

            <div className="mt-5">
              <button onClick={getPosts} className="btn btn-dark btn-lg w-100">
                Get Posts from server
              </button>
              <button
                onClick={() => setShowingCreateNewPostForm(true)}
                className="btn btn-secondary btn-lg w-100 mt-4"
              >
                Create new Post
              </button>
            </div>
          </div>
          )}
          
          {(posts.length > 0 && showingCreateNewPostForm === false && postCurrentlyBeingUpdated === null) && renderPostsTable()}



          {showingCreateNewPostForm && <PostCreateFrom onPostCreated={onPostCreated}/>}



            {postCurrentlyBeingUpdated !== null && <PostUpdateFrom post={postCurrentlyBeingUpdated} onPostUpdated = {onPostUpdated}/>}


        </div>
      </div>
    </div>
  );

  function renderPostsTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">PostId (PK)</th>
              <th scope="col">Title</th>
              <th scope="col">Content</th>
              <th scope="col">CRUD Operations</th>
            </tr>
          </thead>
          <tbody>
            {posts.map((post) => {
              return (
                <tr key={post.postId}>
                  <th scope="row">{post.postId}</th>
                  <td>{post.title}</td>
                  <td>{post.content}</td>
                  <td>
                    <button onClick={()=> setPostCurrentlyBeingUpdated(post)} className="btn btn-dark btn-lg mx-3 my-3">
                      Update
                    </button>
                    <button onClick={()=> { if(window.confirm(`Are you sure to delete ${post.title} ?`)) deletePost(post.postId)}} className="btn btn-secondary btn-lg">Delete</button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>

        <button
          onClick={() => setPosts([])}
          className="btn btn-dark btn-lg w-100"
        >
          Empty posts array
        </button>
      </div>
    );
  }


  function onPostCreated(createdPost) {

    setShowingCreateNewPostForm(false)

    if (createdPost === null) {
      return
    }

    alert(`Post successfully creater. Benlow tablw wrote ${createdPost.title}`)

    getPosts();
  }

  function onPostUpdated(updatedPost) {
    setPostCurrentlyBeingUpdated(null)

    if (updatedPost === null) {
      return
    }

    let postsCopy = [...posts]
    
    const index = postsCopy.findIndex((postsCopyPost, currentIndex)=> {
      if(postsCopyPost.postId === updatedPost.postId) {
        return true
      }
    })
    if(index !== -1) {
      postsCopy[index] = updatedPost
    }

    setPosts(postsCopy)

    alert(`Post successfly updated. "${updatedPost.title}"`)
  }


  function onPostDeleted(deletePostPostId) {

    

    let postsCopy = [...posts]
    
    const index = postsCopy.findIndex((postsCopyPost, currentIndex)=> {
      if(postsCopyPost.postId === deletePostPostId) {
        return true
      }
    })
    if(index !== -1) {
      postsCopy.splice(index, 1)
    }

    setPosts(postsCopy)

    alert(`Post successfly deleted.`)
  }
}
