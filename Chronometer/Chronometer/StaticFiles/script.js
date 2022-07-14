var timersDictionary = {};
var counter = 1;

function RefreshTimerValues(timer, chronoId) {
    timer.addEventListener('secondTenthsUpdated', () => {

        let minutesElement = document.querySelector(`#minutes${chronoId}`);
        let secondsElement = document.querySelector(`#seconds${chronoId}`);
        let milisecondsElement = document.querySelector(`#miliseconds${chronoId}`);

        const obj = timer.getTimeValues();
        minutesElement.innerText = String(obj.minutes).padStart(2, '0');
        secondsElement.innerText = String(obj.seconds).padStart(2, '0');
        milisecondsElement.innerText = String(obj.secondTenths);
    });
}

function StartOrStopTimer(element) {
    let currentIdNum = element.dataset.cid;

    let timer = timersDictionary[currentIdNum];

    if (element.className == 'start-button') {

        element.classList.remove('start-button');
        element.classList.add('stop-button');
        timer.start({
            precision: 'secondTenths'
        });

    } else if (element.className == 'stop-button') {

        element.classList.remove('stop-button');
        element.classList.add('start-button');
        timer.pause();
    }
}

function ResetTimer(element) {
    let currentIdNum = element.dataset.cid;
    let timer = timersDictionary[currentIdNum];
    let minutesElement = document.querySelector(`#minutes${currentIdNum}`);
    let secondsElement = document.querySelector(`#seconds${currentIdNum}`);
    let milisecondsElement = document.querySelector(`#miliseconds${currentIdNum}`);

    let main_button = document.querySelector('#main-button-id' + currentIdNum);

    timer.reset();
    timer.stop();

    if (main_button.className == 'stop-button') {
        main_button.classList.remove('stop-button');
        main_button.classList.add('start-button');
    }

    minutesElement.innerText = '00';
    secondsElement.innerText = '00';
    milisecondsElement.innerText = '0';
}

var AddHtmlTags = () => {
    var div = document.createElement('div');
    div.setAttribute('id', `wrap${counter}`);
    div.setAttribute('class', `all-timer-info-wrap`);

    div.innerHTML = `<p id="timer-info-paragraph${counter}">
                <span id="minutes${counter}">00</span> :
                <span id="seconds${counter}">00</span> :
                <span id="miliseconds${counter}">0</span>
            </p>
            <button id="main-button-id${counter}" data-cid=${counter} class="start-button" onclick="StartOrStopTimer(this)"></button>
            <button id="reset-button-id${counter}" data-cid=${counter} class="reset-button" onclick="ResetTimer(this)">Reset</button>
            <button id="remove-button-id${counter}" data-cid=${counter} class="remove-button" onclick="RemoveTimer(this)">Remove</button>
        `;

    document.querySelector('.mega-wrap').appendChild(div);

    timersDictionary[counter] = new easytimer.Timer();

    RefreshTimerValues(timersDictionary[counter], counter);
    counter++;
}

var RemoveTimer = (element) => {
    element.parentNode.remove();
    timersDictionary[element.dataset.cid].stop();
    delete timersDictionary[element.dataset.cid];
}