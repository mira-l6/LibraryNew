let chatbotContainer = document.querySelector('.chatbot-activeSpace');
let chatbotButton = document.querySelector('.cb-renderBtn');

alert('hehheheeh')

chatbotButton.addEventListener('click', renderChatbot);
function renderChatbot(e) {

    let initialQuestion = document.querySelector('#cb-initial-input').value ? true : false;
    let question = document.querySelector('#cb-initial-input').value.trim();

    document.querySelector('#cb-initial-input').setAttribute('placeholder', 'Ask a question...');

    if (initialQuestion) {

        if (chatbotContainer.style.display !== 'block') { chatbotContainer.style.display = 'block'; }

        chatbotContainer.querySelector('.response-avatar p').textContent = question;

        console.log(question)

        fetchAnswerFromServer(question);

        document.querySelector('#cb-initial-input').value = '';
        let hideButton = chatbotContainer.querySelector('.chatbot-title button');
        hideButton.addEventListener('click', handleHideChatbot);

    } else {

        document.querySelector('#cb-initial-input').setAttribute('placeholder', 'If you want to activate the chatbot you should type in a question...');
    }
}

function handleHideChatbot(e) {

    const titleContainer = e.currentTarget.parentNode;
    const mainContainer = titleContainer.parentNode;
    mainContainer.style.display = 'none';
}

function fetchAnswerFromServer(question) {

    console.log(question)

    fetch('/ChatBot', {
        method: 'POST',
        body: JSON.stringify({ question }),
        headers: { 'Content-Type': 'application/json' }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {

            let chatAnswer = data["answer"];

            const responseElement = document.querySelector('.response-text');
            responseElement.innerHTML = chatAnswer;  // Use the response field
        })
        .catch(err => throw new Error(err);
}