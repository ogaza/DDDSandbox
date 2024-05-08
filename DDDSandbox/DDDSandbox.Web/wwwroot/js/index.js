window.addEventListener("DOMContentLoaded", startTheApp);

var form;

function startTheApp() {
  console.log("this is the DDD sandob app client");

  form = document.querySelector("form");
  form.addEventListener("submit", hendleSubmit);
}

function hendleSubmit(e) {
  e.preventDefault();

  let requestBody = createRequestBodyFromFormData(new FormData(form));

  requestBody = { request: { userId: "2853320d-3a16-490a-9683-94316c81d049" } };

  sendRequest(requestBody, onCreated);
}

async function sendRequest(requestBody, callback) {
  const response = await fetch("http://localhost:5014/orders", {
    method: "POST",
    headers: {
      "Content-type": "application/json"
    },
    body: JSON.stringify(requestBody)
  });

  callback(response);
}

function createRequestBodyFromFormData(formData) {
  const body = {};

  formData.entries().forEach(function (entry) {
    const name = entry[0];
    const value = entry[1];

    body[name] = value;
  });

  return body;
}

function createBodyPartFromFormEntry(entry) {
  const partOne = encodeURIComponent(entry[0]);
  const partTwo = encodeURIComponent(entry[1]);

  return `${partOne}=${partTwo}`;
}

async function onCreated(response) {
  console.log(response);

  // const responseJson = await response.json();
  // console.log(responseJson);
  // result.innerHTML = `server responded with: ${JSON.stringify(responseJson)} `;

  // setTimeout(() => {
  //   window.location.href = responseJson.redirectUrl
  //     ? responseJson.redirectUrl
  //     : "/";
  // }, 1000);
}

// const response = await fetch("http://localhost:5014/weatherforecast");
// const result = await response.json();

// console.log(result);
