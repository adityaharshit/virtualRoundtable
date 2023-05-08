fetch("navbar.html")
  .then((response) => response.text())
  .then((data) => {
    document.getElementById("ind-nav").innerHTML = data;
  });

const optionsCarData = {
  method: "GET",
  headers: {
    "X-RapidAPI-Key": "0711d027e6mshe99f7b3ccdf3a30p1efe41jsn28f3addd6d52",
    "X-RapidAPI-Host": "car-data.p.rapidapi.com",
  },
};

const cards = document.querySelector(".cards");
const cardsScroll = document.querySelector(".cards-scroll");
const cardsHorizontal = document.querySelector(".card-horizontal");
const cardsModels = document.querySelector(".cards-models");
//function displayCarMakes() {
//  fetch("https://car-data.p.rapidapi.com/cars/makes", optionsCarData)
//    .then((response) => response.json())
//    .then((response) => {
//      for (let i = 0; i < 20; i++) {
//        let carName = response[i];

//        const htmlText = `<div class="card1">
//      <div class="card1-content">	 
//      <h3 class="card1-heading">${carName}</h3>
//      <p class="card1-body">Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
//        <a href="/forum" class="button">Go</a>
//        </div>
//        </div>`;

//        cards.insertAdjacentHTML("afterbegin", htmlText);
//        const htmlTextSCrollCards = `
//      <button class="scroll-link" name=${carName}>${carName}</button>
//      `;

//        cardsScroll.insertAdjacentHTML("afterbegin", htmlTextSCrollCards);
//        const img = document.querySelector(".card1");
//        const optionsUnsplash = {
//          method: "GET",
//          headers: {
//            "X-RapidAPI-Key": "bETa9G8FiJIPXm9P5qFZK9RSfgMPrMsnXv127dnRw7U",
//          },
//        };
//        fetch(
//          `https://api.unsplash.com/search/photos?&query=${carName}%20cars&client_id=bETa9G8FiJIPXm9P5qFZK9RSfgMPrMsnXv127dnRw7U`
//        )
//          .then((response) => response.json())
//          .then((response) => {
//            // console.log(response);
//            let allImages = response.results[0];
//            img.style.backgroundImage = "url(" + allImages.urls.regular + ")";
//          })
//          .catch((err) => console.error(err));
//      }
//      divEventListener();
//    })
//    .catch((err) => console.error(err));
//}
//displayCarMakes();

//function divEventListener() {
//  const card1 = document.querySelectorAll(".card1");
//  // console.log(card1);
//  card1.forEach((element) => {
//    element.addEventListener("click", () => {
//      const selectedCar = element.firstElementChild.firstElementChild.innerHTML;
//      cards.classList.toggle("hidden");
//      cardsHorizontal.classList.toggle("hidden");
//      document
//        .querySelector(`.scroll-link[name=${selectedCar}]`)
//        .classList.toggle("scroll-link-active");
//      cards.innerHTML = "";
//      displayCarModels(selectedCar);
//    });
//  });

//  const scrollLink = document.querySelectorAll(".scroll-link");
//  // console.log(scrollLink);
//  scrollLink.forEach((element) => {
//    element.addEventListener("click", () => {
//      // console.log("Helo");
//      const selectedCar = element.name;
//      document
//        .querySelector(".scroll-link-active")
//        .classList.toggle("scroll-link-active");
//      document
//        .querySelector(`.scroll-link[name=${selectedCar}]`)
//        .classList.toggle("scroll-link-active");

//      cardsModels.innerHTML = "";
//      displayCarModels(selectedCar);
//    });
//  });
//}

//function displayCarModels(carName) {
//  const options = {
//    method: "GET",
//    headers: {
//      "X-RapidAPI-Key": "0711d027e6mshe99f7b3ccdf3a30p1efe41jsn28f3addd6d52",
//      "X-RapidAPI-Host": "car-data.p.rapidapi.com",
//    },
//  };

//  fetch(
//    `https://car-data.p.rapidapi.com/cars?limit=20&page=0&make=${carName}&year=2020`,
//    options
//  )
//    .then((response) => response.json())
//    .then((response) => {
//      for (let i = 0; i < 20; i++) {
//        let modelName = response[i].model;
//        const htmlText = `<div class="card1">
//      <div class="card1-content">	 
//        <h3 class="card1-heading">${modelName}</h3>
//        <p class="card1-body">Lorem ipsum dolor sit amet consectetur adipisicing elit.</p>
//        <a href="" class="button">Go</a>
//        </div>
//        </div>`;
//        cardsModels.insertAdjacentHTML("afterbegin", htmlText);
//        const img = document.querySelector(".card1");
//        // console.log(img);
//        const optionsUnsplash = {
//          method: "GET",
//          headers: {
//            "X-RapidAPI-Key": "bETa9G8FiJIPXm9P5qFZK9RSfgMPrMsnXv127dnRw7U",
//          },
//        };
//        fetch(
//          `https://api.unsplash.com/search/photos?&query=${modelName} ${carName}%20cars&client_id=bETa9G8FiJIPXm9P5qFZK9RSfgMPrMsnXv127dnRw7U`
//        )
//          .then((response) => response.json())
//          .then((response) => {
//            // console.log(response);
//            let allImages = response.results[0];
//            // console.log(allImages.urls.regular);
//            img.style.backgroundImage = "url(" + allImages.urls.regular + ")";
//          })
//          .catch((err) => console.error(err));
//      }
//    })
//    .catch((err) => console.error(err));
//}

// ------------------------------------------------------------------------------------------- sub-topics ---------------------------------------------------

const forumLeftList = document.querySelectorAll(".forum-left-subt-list>li");
function sendToNormal() {
  const forumTopicLists = document.querySelectorAll(
    ".forum-left-subt-list-items"
  );
  forumTopicLists.forEach((element) => {
    element.classList.add("hidden");
  });
  const faChevDown = document.querySelectorAll(".fa-chevron-down");
  faChevDown.forEach((element) => {
    element.classList.remove("rotate-180");
  });
}
let randomElement;
forumLeftList.forEach((element) => {
  element.addEventListener("click", () => {
    if (element != randomElement) sendToNormal();
    element.lastElementChild.classList.toggle("rotate-180");
    element.nextElementSibling.classList.toggle("hidden");
    randomElement = element;
  });
});

// ------------------------------------------------------------------------------------------- tags ---------------------------------------------------
const forumTags = document.querySelectorAll(".forum-right-tags > p");
forumTags.forEach((element, index) => {
  element.addEventListener("click", () => {
    const tagName = element.textContent;
    if (element.classList.contains("p-active")) {
      element.innerHTML = tagName;
    } else {
      element.insertAdjacentHTML("beforeend", '<i class="fa-solid fa-xmark">');
    }
    element.classList.toggle("p-active");
  });
});

const forumFilter = document.querySelectorAll(".forum-right-fields-ul>li ");
// console.log(forumFilter);
forumFilter.forEach((element) => {
  element.addEventListener("click", () => {
    forumFilter.forEach((element) => {
      element.classList.remove("forum-right-active");
    });
    element.classList.toggle("forum-right-active");
  });
});

// ------------------------------------------------------------------------------------ forum menu -----------------------------------------------
const forumMenu = document.querySelectorAll(".forum-post-menu");
console.log(forumMenu);
forumMenu.forEach((elements) => {
  elements.addEventListener("click", function (event) {
    event.stopPropagation();
    console.log(elements.parentElement.nextElementSibling);
    elements.parentElement.nextElementSibling.classList.toggle("hidden");
  });
});

document.querySelector("body").addEventListener("click", () => {
  const forumMenu = document.querySelectorAll(".forum-post-bottom-menu");
  forumMenu.forEach((element) => {
    element.classList.add("hidden");
  });
});

const likePost = document.querySelectorAll(".fa-thumbs-up");
const dislikePost = document.querySelectorAll(".fa-thumbs-down");
console.log(likePost);
likePost.forEach((element, index) => {
  element.addEventListener("click", () => {
    element.classList.toggle("post-liked-disliked");
    let noOfLike = Number(element.parentElement.nextElementSibling.textContent);
    if (element.classList.contains("post-liked-disliked")){
      if (dislikePost[index].classList.contains("post-liked-disliked")){
        dislikePost[index].classList.remove("post-liked-disliked");
        noOfLike = noOfLike + 2;
      }
      else{
        noOfLike++;
      }
    }
    else {
      noOfLike = noOfLike - 1;
    }
    
      element.parentElement.nextElementSibling.textContent = noOfLike;
  });
});
dislikePost.forEach((element, index) => {
  element.addEventListener("click", () => {
    element.classList.toggle("post-liked-disliked");
      let noOfLike = Number(element.parentElement.previousElementSibling.textContent);
    if (element.classList.contains("post-liked-disliked")){
      if (likePost[index].classList.contains("post-liked-disliked")){
        likePost[index].classList.remove("post-liked-disliked");
        noOfLike = noOfLike - 2;
      }
      else{ 
        noOfLike = noOfLike - 1;
      }
    }
    else
     noOfLike = noOfLike + 1;
    element.parentElement.previousElementSibling.textContent = noOfLike;
  });
});
