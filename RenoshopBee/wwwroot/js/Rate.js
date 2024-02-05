const stars = document.querySelectorAll('.star');
let selectedStar = null;
stars.forEach(star => {
    star.addEventListener('mouseover', () => {
        selectedStar = null
        const starValue = star.getAttribute('data-star');
        for (let i = 0; i < starValue; i++) {
            stars[i].classList.add('selected');
        }
    });

    star.addEventListener('mouseout', () => {
        if (selectedStar === null) {
            stars.forEach(s => s.classList.remove('selected'));
        }
    });
    star.addEventListener('click', () => {
        const starValue = star.getAttribute('data-star');
        if (selectedStar === starValue) {
            selectedStar = null;
            star.classList.remove('selected');
        } else {
            selectedStar = starValue;
        }
    });
});


let productID = document.querySelector(".productId").value
function AddReview() {
    let starNum = 0;
    let RevBody = document.querySelector(".reviewTA").value
    stars.forEach(star => {
        const styles = window.getComputedStyle(star);
        var color = styles.getPropertyValue('color');
        if (color != "rgb(169, 169, 169)") {
            starNum = starNum+1;
        }
    })
    dataOPJ = {
        ProductRate: starNum,
        ReviewBody: RevBody,
        ProductId: productID
    }
    $.ajax({
        url: `/Products/Details/AddReview`,
        type: 'POST',
        data: dataOPJ,
        success: function (data) {
            reloadPartialView(productID);
            selectedStar = null;
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}
function reloadPartialView(id) {
    $.ajax({
        url: `/Products/GetReviews/${id}`, // Replace 'ControllerName' with your actual controller name
        type: 'GET',
        success: function (data) {
            // Replace the content of the target element with the retrieved partial view
            $('#PartRev').html(data);
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });

}
function deleteReview(ReviewId) {
    fetch(`/Products/Details/DeleteReview/${ReviewId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            if (response.ok) {
                // Wishlist item deleted successfully
                // You can perform additional actions here if needed
                location.reload("true")
                reloadPartialView(productID);
                var btn = document.getElementById("Reviews-tab")
                btn.click()
                // Perform any necessary UI updates
                // For example, remove the deleted item from the DOM
            } else {
                // Handle error response
                console.error('Error deleting wishlist item');
            }
        })
        .catch(error => {
            // Handle network or other errors
            console.error('Error deleting wishlist item', error);
        });
}
function onHoverStarsEdit() {
    const stars = document.querySelectorAll('.star');
    let selectedStar = null;
    stars.forEach(star => {
        star.addEventListener('mouseover', () => {
            selectedStar = null
            const starValue = star.getAttribute('data-star');
            for (let i = 0; i < starValue; i++) {
                stars[i].classList.add('selected');
            }
        });

        star.addEventListener('mouseout', () => {
            if (selectedStar === null) {
                stars.forEach(s => s.classList.remove('selected'));
            }
        });
        star.addEventListener('click', () => {
            const starValue = star.getAttribute('data-star');
            if (selectedStar === starValue) {
                selectedStar = null;
                star.classList.remove('selected');
            } else {
                selectedStar = starValue;
            }
        });
    });
}
function supmitUpdatedReview() {
    let productID = document.querySelector(".productId").value
    let stars = document.querySelectorAll('.star');
    var reviewId = $('.RevId').val();
    var revBody = $('.RevBody').val();
    let starNum = 0;
    stars.forEach(star => {
        const styles = window.getComputedStyle(star);
        var color = styles.getPropertyValue('color');
        if (color != "rgb(169, 169, 169)") {
            starNum = starNum + 1;
        }
    })
    var dataOPJ = {
        ReviewId: reviewId,
        ReviewBody: revBody,
        reviewRate: starNum
    };
    $.ajax({
        url: `/Products/Details/EditReview`,
        type: 'POST',
        data: dataOPJ,
        success: function (data) {
            reloadPartialView(productID);
            selectedStar = null;
        },
        error: function (error) {
            console.error('Error:', error);
        }
    });
}