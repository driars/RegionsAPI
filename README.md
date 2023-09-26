<a name="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h1 align="center">Regions with API</h1>
  <p align="center">
    An ASP.NET Core WebAPI application that seeds data from csv files and provides region and employee endpoints.
    <br />
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## About The Project

Parse .csv files and keep regions and employees data in memory to quickly build the HTTP Get request. And use background service to keep data to SQL Server database. Provide some HTTP endpoints like getting employees data from region_id.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

- ![.NET][.NET]
- ![MicrosoftSQLServer][MicrosoftSQLServer]
- ![JavaScript][JavaScript]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->

## Getting Started

### Installation

1. Build the backend project
   ```sh
   dotnet build ./RegionsAPI/
   ```
2. Init Database
   ```sh
   dotnet ef database update --project ./RegionsAPI/Data -s ./RegionsAPI/RegionsAPI
   ```
2. Copy SeedData to `bin` directory
   ```sh
   cp -r ./RegionsAPI/SeedData/ ./RegionsAPI/RegionsAPI/bin/Debug
   ```
3. Run the backend project
   ```sh
   cd ./RegionsAPI/RegionsAPI/bin/Debug/net7.0

   ./RegionsAPI.exe
   ```
6. Open Front End files
   ```sh
   ./FrontEnd/index.html
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ROADMAP -->

## Roadmap

- [x] Parse CSV data in memory
- [x] Use background service to save cache data to database
- [x] Utilize EF Core to communicate with the database
- [x] Implement some HTTP endpoints
- [x] Build `region` and `employee` HTTP Post endpoints to persist new data
- [x] Build simple frontend pages with plain JavaScript

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->

## Contact

Arseniy - driars0329@gmail.com

Project Link: [https://github.com/driars/books-list](https://github.com/driars/books-list)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->

[.Net]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[MicrosoftSQLServer]: https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white
[JavaScript]: https://img.shields.io/badge/javascript-%23323330.svg?style=for-the-badge&logo=javascript&logoColor=%23F7DF1E