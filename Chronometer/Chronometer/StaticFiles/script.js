var hours = document.querySelector('#hours');
var minutes = document.querySelector('#minutes');
var seconds = document.querySelector('#seconds');
var miliseconds = document.querySelector('#miliseconds');

var timer = new easytimer.Timer();
var mainButton = document.getElementById('main-button-id');
var resetButton = document.getElementById('reset-button-id');

timer.addEventListener('secondTenthsUpdated', () => {
    const obj = timer.getTimeValues();

    hours.innerText = String(obj.hours).padStart(2, '0');
    minutes.innerText = String(obj.minutes).padStart(2, '0');
    seconds.innerText = String(obj.seconds).padStart(2, '0');
    miliseconds.innerText = String(obj.secondTenths);
});

mainButton.addEventListener('click', () => {
    if (mainButton.className == 'start-button') {
        mainButton.classList.remove('start-button');
        mainButton.classList.add('stop-button');
        timer.start({
            precision: 'secondTenths'
        });
    } else if (mainButton.className == 'stop-button') {
        mainButton.classList.remove('stop-button');
        mainButton.classList.add('start-button');
        timer.pause();
    }
});

resetButton.addEventListener('click', () => {
    timer.reset();
    timer.stop();

    if (mainButton.className == 'stop-button') {
        mainButton.classList.remove('stop-button');
        mainButton.classList.add('start-button');
    }

    hours.innerText = '00';
    minutes.innerText = '00';
    seconds.innerText = '00';
    miliseconds.innerText = '0';
});