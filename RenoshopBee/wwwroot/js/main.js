console.log("asd1")
let wishlistBtn = document.querySelectorAll(".add-to-wishlist")
let EditBtns = document.querySelector(".editBtn")
//EditBtns.onclick = function () {
//    document.querySelector("")
//}
function EditReview(id) {
    document.querySelector(".review-text-" + id).style.display = "none";
    document.querySelector(`.review-form-${id}`).style.display = "block";
}
//wishlistBtn.style.background = "blue"
setTimeout(function () {
    document.querySelector(".notfication").style.top = '-50px';
}, 1000);
wishlistBtn.onclick = function () {
    console.log("asd")
    document.querySelector(".notfication").style.top = "20px"
}
document.addEventListener("click", function (e) {
    console.log("asd")
    wishlistBtn.classList.toggle('show')
})
setTimeout(() => {
    let cartButton = document.querySelector(".svg-inline--fa.fa-cart-shopping.button");
    let openCartPageButton = document.querySelector(" .card-items .view-cart");
    cartButton.onclick = function(){
        document.querySelector(".user-nav").classList.remove("show");
        //document.querySelector(".search-form").classList.remove("show");
        document.querySelector(".card-items").classList.toggle("show");
        openCartPageButton.onclick = function(){
            window.location.assign("cart.html")
        } 
    }
    //let searchButton = document.querySelector(".search-bar svg");
    //searchButton.onclick = function(){
    //    document.querySelector(".user-nav").classList.remove("show");
    //    document.querySelector(".card-items").classList.remove("show");
    //    document.querySelector(".search-form").classList.toggle("show");
    //}
    let userInfo = document.querySelector(".min-info");
    userInfo.onclick = function(){
        //document.querySelector(".search-form").classList.remove("show");
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
            //document.querySelector(".search-form").classList.remove("show");
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

//function updateReview(id) {
//    var reviewId = document.querySelector(`.RevId`).value;
//    var revBody = document.querySelector('.RevBody').value;
//    console.log("XXX")
//    // Create an object to send as JSON
//    var data = {
//        ReviewId: reviewId,
//        ReviewBody: revBody
//    };

//    // Make the AJAX request
//    fetch(`/Products/Details/EditReview/${id}`, {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(data)
//    })
//        .then(response => {
//            console.log("GG")
//            if (response.ok) {
//                console.log("OK")
//               return response.json();
//            }
//        })
//        .then(data => { console.log(data) })
//        //.then(data => {
//        //    // Handle the response from the server
//        //    //if (data.success) {
//        //    //    alert(data.message);
//        //    //    // Optionally, update the UI or perform other actions
//        //    //} else {
//        //    //    alert('Error updating review.');
//        //    //}
//        //})
//        .catch(error => {
//            console.error('Error:', error);
//        })
function ViewEditedReview(id, newReview) {
    reviewText = document.querySelector(".review-text-" + id);
    reviewForm = document.querySelector(`.review-form-${id}`);
    reviewText.textContent = newReview;
    reviewText.style.display = "block";
    reviewForm.style.display = "none";
    console.log(newReview)
}
    function updateReview(id) {
        var reviewId = $('.RevId').val();
        var revBody = $('.RevBody').val();
        console.log(revBody)

        // Create an object to send as JSON
        var data = {
            ReviewBody: revBody
        };

        $.ajax({
            url: `/Products/Details/EditReview`,
            type: 'POST',
            data: {
                ReviewId: reviewId,
                ReviewBody: revBody
            },
            success: function (response) {
                console.log("asdsad")
                ViewEditedReview(id, revBody);
                // Handle the response from the server here
            },
            error: function (error) {
                console.error('Error:', error);
            }
        });
}
function updateRev(id){
    var reviewId = $('.RevId').val();
    $.ajax({
        url: `/Products/Details/GetEditReview/${id}`,
        type: 'GET',
        data: { reviewID: reviewId },
        success: function (data) {
            $('#partView').html(data);
            // Handle the response from the server here
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}





