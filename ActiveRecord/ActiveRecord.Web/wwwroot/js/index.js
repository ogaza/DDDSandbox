window.addEventListener("DOMContentLoaded", startTheApp);

async function startTheApp() {
  console.log("this is the Active Record sandbox page");

  const commentsApi = createApi("comment");
  const postsApi = createApi("post");

  const posts = await postsApi.getAll();
  console.log("all posts:", posts);

  let newComment = {
    text: "comment from the ui",
    post: {
      id: 1
    }
  };
  const newCommentId = await commentsApi.save(newComment);
  console.log("newCommentId", newCommentId);

  newComment = {
    ...newComment,
    id: newCommentId,
    text: "updated comment from the ui"
  };

  const updatedCommentId = await commentsApi.save(newComment);
  console.log("updatedCommentId", updatedCommentId);

  const postDeleted = await commentsApi.delete(newCommentId);
  console.log("postDeleted", postDeleted);
}

const apiUrl = "http://localhost:5236/blog";

function createApi(entityName) {
  return {
    getAll: function () {
      return getAll(entityName);
    },
    delete: function (id) {
      return deleteEntity(entityName, id);
    },
    save: function (entity) {
      return saveEntity(entityName, entity);
    }
  };
}

async function deleteEntity(entityName, id) {
  const response = await fetch(`${apiUrl}/${entityName}s/${id}`, {
    method: "DELETE"
  });
  const responseJson = await response.json();

  return responseJson;
}

async function saveEntity(entityName, entity) {
  const response = await fetch(`${apiUrl}/${entityName}s/`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
      // 'Content-Type': 'application/x-www-form-urlencoded',
    },
    body: JSON.stringify(entity)
  });
  const responseJson = await response.json();

  return responseJson;
}

async function getAll(entityName) {
  const response = await fetch(`${apiUrl}/${entityName}s`);
  const responseJson = await response.json();

  return responseJson;
}
