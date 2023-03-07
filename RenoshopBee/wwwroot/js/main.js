setTimeout(() => {
    let cartButton = document.querySelector(".svg-inline--fa.fa-cart-shopping.button");
    let openCartPageButton = document.querySelector(" .card-items .view-cart");
    cartButton.onclick = function(){
        document.querySelector(".user-nav").classList.remove("show");
        document.querySelector(".search-form").classList.remove("show");
        document.querySelector(".card-items").classList.toggle("show");
        openCartPageButton.onclick = function(){
            window.location.assign("cart.html")
        } 
    }
    let searchButton = document.querySelector(".search-bar svg");
    searchButton.onclick = function(){
        document.querySelector(".user-nav").classList.remove("show");
        document.querySelector(".card-items").classList.remove("show");
        document.querySelector(".search-form").classList.toggle("show");
    }
    let userInfo = document.querySelector(".min-info");
    userInfo.onclick = function(){
        document.querySelector(".search-form").classList.remove("show");
        document.querySelector(".card-items").classList.remove("show");
        document.querySelector(".user-nav").classList.toggle("show");
        console.log("asd");
    }
    let sectionSort = document.querySelector(".sort-head");
    sectionSort.onclick = function(){
        console.log("dasdas");
        document.querySelector(".sort").classList.toggle("show");
    }
    document.addEventListener("click",function(e){
        if (e.path.length < 11) {
            document.querySelector(".user-nav").classList.remove("show");
            document.querySelector(".card-items").classList.remove("show");
            document.querySelector(".search-form").classList.remove("show");
        }
        if(document.querySelector(".user-nav").classList.contains("show")){
            document.querySelector(".svg-inline--fa.fa-chevron-down").style.transform = `rotate(180deg)`;
        }
        else{
            document.querySelector(".svg-inline--fa.fa-chevron-down").style.transform = `rotate(0deg)`;
        }
    })
}, 100);
let darkModeBtn = document.querySelector(".Dark-mode-switcher");
darkModeBtn.onclick = function(){
    darkModeBtn.classList.toggle("toggle");
}


//scroll to top button
let stpBtn = document.createElement("button");
        stpBtn.classList.add("btn","btn-outline-primary","position-fixed","bottom-0","end-0","me-3","mb-3","fs-5","d-none");
        stpBtn.innerHTML = `<i class="fas fas fa-chevron-circle-up"></i>`;
        stpBtn.onclick = function(){
            window.scrollTo({
                top:0,
                left:0,
                behavior:"smooth"
            })
        }
        document.body.appendChild(stpBtn);
window.onscroll = function(){
    if (scrollY>=700){
        stpBtn.classList.remove("d-none");
    }
    else{
        stpBtn.classList.add("d-none");
    }

}
let sectionSorting = document.querySelector(".filter"); 
sectionSorting.onclick = function () {
    document.querySelector(".section-sort").classList.toggle("show");
}

function categoryChoose() {
    var categoryValue = document.querySelector('.category').value;

    if ((categoryValue == 'Men' || categoryValue == 'Women' || categoryValue == 'Kids')) {
        if (!(document.querySelector('.size').className.endsWith('show'))) {
            document.querySelector('.size').classList.toggle('show');
        }
    }
    else {
        document.querySelector('.size').classList.remove('show');
    }
}