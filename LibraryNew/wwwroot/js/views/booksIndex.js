
const staticFeatures = document.querySelector('.static-features');
const booksDisplay = document.querySelector('.book-display');
const bookShelfDisplay = document.querySelector('.bookshelf-display');

const hideShowButton = document.querySelector('#hideOrShowStaticFeatures');
//hideShowButton.addEventListener('click', showOrHideStaticFeatures)

function showOrHideStaticFeatures(e) {

    if (staticFeatures.style.display !== 'none') {

        staticFeatures.style.display = 'none';
        booksDisplay.style.width = '100%';
        bookShelfDisplay.style.gridTemplateColumns = 'repeat(5, 1fr)';
        booksDisplay.style.borderRight = 'none';

    } else {

        staticFeatures.style.display = 'block';
        booksDisplay.style.width = '60%';
    }
}