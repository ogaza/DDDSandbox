window.addEventListener("DOMContentLoaded", startTheApp);

function startTheApp() {
  console.log("this is the DDD sandob app client");
}

const response = await fetch("http://localhost:5014/weatherforecast");
const result = await response.json();

console.log(result);
