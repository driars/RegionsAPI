const apiUrlForRegion = "https://localhost:5329/regions";
const apiUrlForEmployee = "https://localhost:5329/region/{0}/employees";

document.addEventListener("DOMContentLoaded", function () {
  fetchAndGenerateRegionColumns();

  const regionTbody = document.querySelector("#region tbody");

  regionTbody.addEventListener("click", function (event) {
    if (
      event.target.tagName === "TD" &&
      event.target.parentNode.dataset.regionId
    ) {
      const regionId = event.target.parentNode.dataset.regionId;
      fetchAndGenerateEmployeeColumns(regionId);
    }
  });
});

function fetchAndGenerateRegionColumns() {
  // Use the fetch() function to make a GET request to the API
  fetch(apiUrlForRegion)
    .then((response) => {
      // Check if the response status is OK (200)
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      // Parse the response body as JSON
      return response.json();
    })
    .then((data) => {
      // Do something with the data received from the API
      const regionTable = document.getElementById("region");
      const tbody = regionTable.querySelector("tbody");

      tbody.innerHTML = "";

      data.forEach((item) => {
        const row = document.createElement("tr");

        row.dataset.regionId = item.id;
        row.addEventListener("click", function () {
          fetchAndGenerateEmployeeColumns(item.id);
        });

        const nameCell = document.createElement("td");
        const parentCell = document.createElement("td");

        nameCell.textContent = item.name;
        parentCell.textContent = item.parentName;

        row.appendChild(nameCell);
        row.appendChild(parentCell);
        tbody.appendChild(row);
      });
    })
    .catch((error) => {
      // Handle any errors that occurred during the fetch
      console.error("Fetch error:", error);
    });
}

function fetchAndGenerateEmployeeColumns(regionId) {
  fetch(apiUrlForEmployee.replace("{0}", regionId))
    .then((response) => response.json())
    .then((data) => {
      const employeeTable = document.getElementById("employee");
      const employeeTbody = employeeTable.querySelector("tbody");

      employeeTbody.innerHTML = "";

      data.forEach((item) => {
        const row = document.createElement("tr");

        const nameCell = document.createElement("td");
        const regionCell = document.createElement("td");

        nameCell.textContent = item.name + " " + item.surName;
        regionCell.textContent = item.regionName;

        row.appendChild(nameCell);
        row.appendChild(regionCell);
        employeeTbody.appendChild(row);
      });
    })
    .catch((error) => {
      console.log(error);
    });
}
