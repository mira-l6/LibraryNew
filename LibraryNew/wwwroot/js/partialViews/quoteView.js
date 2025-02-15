
let quoteText = document.querySelector('.quote-text');
let quoteAuthor = document.querySelector('.quote-author');
let quoteResource = document.querySelector('.quote-resource');

async function fetchQuote() {

    const response = await fetch('/Quote/GetQuote');
    const data = await response.json();

    return Object.values(data)[0];
}

async function setClientView() {

    quoteText.textContent = '';
    quoteAuthor.textContent = '';

    const quote = await fetchQuote();
    console.log(quote);

    quoteText.textContent = quote.quote + "";

    if (quote.author.split(',')[1] === undefined) {

        quoteResource.textContent = ', unknown resource'; quoteAuthor.textContent = ` - ${quote.author} `;
    }
    else {

        quoteAuthor.textContent = ` - ${quote.author.split(',')[0]} `;
        quoteResource.textContent = `, "${quote.author.split(',')[1]}"`;
    }

    sessionStorage.setItem('quote', quote.quote);
    sessionStorage.setItem('quote-author', quote.author);

}

setClientView();

