window.addEventListener("DOMContentLoaded", startTheApp);

async function startTheApp() {
  console.log("this is the Active Record sandbox page");

  const commentsApi = createApi("comment");
  const postsApi = createApi("post");

  await postsApi.getAll();

  const commentId = 3;
  await commentsApi.deletecomment(commentId);
}

const apiUrl = "http://localhost:5236/blog";

function createApi(entityName) {
  return {
    getAll: function () {
      return getAll(entityName);
    },
    [`delete${entityName}`]: function (id) {
      return deleteEntity(entityName, id);
    }
  };
}

async function deleteEntity(entityName, id) {
  const response = await fetch(`${apiUrl}/${entityName}s/${id}`, {
    method: "DELETE"
  });
  const responseJson = await response.json();
  console.log(responseJson);
}

async function getAll(entityName) {
  const response = await fetch(`${apiUrl}/${entityName}s`);
  const responseJson = await response.json();
  console.log(responseJson);
}
