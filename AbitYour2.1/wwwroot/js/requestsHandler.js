const requestsTag = document.getElementById('successful-requests');

// Start the connection.
var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Home/Index')
    .build();

var numOfRequests = 0;
// ms.
const durationBeforeNextRequest = 5000; 
var lastUpdate = Date.now() - durationBeforeNextRequest;

function addNumOfRequests() {
    let currentTime = Date.now();
    if (currentTime - lastUpdate < durationBeforeNextRequest)
        return;
    lastUpdate = currentTime;
    connection.invoke("addRequest");
}

function updateRequestsNumber() {
    requestsTag.innerHTML = numOfRequests;
}

connection.on('newRequest', function (requestsNumber) {
    numOfRequests = parseInt(requestsNumber);
    updateRequestsNumber();
});

connection.start()
    .then(function () {
        console.log('connection started');
        connection.invoke("getNumberOfRequests").then(function (requestsNumber) {
            numOfRequests = requestsNumber;
            updateRequestsNumber();
        });
    })
    .catch(error => {
        console.error(error.message);
    });