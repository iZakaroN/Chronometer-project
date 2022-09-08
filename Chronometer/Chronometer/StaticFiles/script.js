var timersDictionary = {};
const route = "../api/chronometer";
let connection = new signalR.HubConnectionBuilder().withUrl('../chats').build();
connection.start().then(() => { console.log('connected'); }).catch((err) => { console.log(err); })



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

function UpdateTimerState(id, isRunning, minutes, seconds, milliseconds) {

    let element = document.querySelector(`#main-button-id${id}`);
    let timer = timersDictionary[id];
    
    if (isRunning) {

        element.classList.remove('start-button');
        element.classList.add('stop-button');

        timer.start({
            precision: 'secondTenths',
            startValues: {
                minutes: timer.getTimeValues().minutes,
                seconds: timer.getTimeValues().seconds,
                secondTenths: timer.getTimeValues().secondTenths
            }
        });

    } else {

        if (minutes == 0 && seconds == 0 && milliseconds == 0) {

            timer.reset();
            timer.stop();

            document.querySelector(`#minutes${id}`).innerText = "00";
            document.querySelector(`#seconds${id}`).innerText = "00";
            document.querySelector(`#miliseconds${id}`).innerText = "0";

            if (element.className == 'stop-button') {
                element.classList.remove('stop-button');
                element.classList.add('start-button');
            }

        } else {

            timer.pause();
            element.classList.remove('stop-button');
            element.classList.add('start-button');
        }
    }
}

function UpdateBackendTimer(element) {

    let currentIdNum = element.dataset.cid;
    let timer = timersDictionary[currentIdNum];

    if (element.className == 'start-button') {

        let chronometerModel = {
            ID: currentIdNum,
            Timer: {
                minutes: timer.getTimeValues().minutes,
                seconds: timer.getTimeValues().seconds,
                milliseconds: timer.getTimeValues().secondTenths
            },
            IsRunning: true
        }

        fetch(route + '/' + currentIdNum, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(chronometerModel)
        });

    } else if (element.className == 'stop-button') {

        let chronometerModel = {
            ID : currentIdNum,
            Timer: {
                minutes: timer.getTimeValues().minutes,
                seconds: timer.getTimeValues().seconds,
                milliseconds: timer.getTimeValues().secondTenths
            },
            IsRunning: false
        }

        fetch(route + '/' + currentIdNum, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(chronometerModel)
        });
    }
}

function ResetTimer(element) {

    let currentIdNum = element.dataset.cid;

    let chronometerModel = {
        ID: currentIdNum,
        Timer: {
            minutes: 0,
            seconds: 0,
            milliseconds: 0
        },
        IsRunning: false
    }

    fetch(route + '/' + currentIdNum, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(chronometerModel)
    });
}

async function CreateChronometer() {
    await fetch(route, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    });
}

function AddChronometerUI(chronometer) {
    var div = document.createElement('div');
    div.setAttribute('id', `wrap${chronometer.id}`);
    div.setAttribute('class', `all-timer-info-wrap`);

    div.innerHTML = `<p id="timer-info-paragraph${chronometer.id}">
                <span id="minutes${chronometer.id}">${String(chronometer.timer.minutes).padStart(2, '0')}</span> :
                <span id="seconds${chronometer.id}">${String(chronometer.timer.seconds).padStart(2, '0')}</span> :
                <span id="miliseconds${chronometer.id}">${String(chronometer.timer.milliseconds)}</span>
            </p>
            <button id="main-button-id${chronometer.id}" data-cid=${chronometer.id} class="start-button" onclick="UpdateBackendTimer(this)"></button>
            <button id="reset-button-id${chronometer.id}" data-cid=${chronometer.id} class="reset-button" onclick="ResetTimer(this)">Reset</button>
            <button id="remove-button-id${chronometer.id}" data-cid=${chronometer.id} class="remove-button" onclick="RemoveTimer(this)">Remove</button>
        `;

    document.querySelector('.mega-wrap').appendChild(div);

    var eTimer = new easytimer.Timer({
        startValues: {
            days: 0,
            hours: 0,
            minutes: chronometer.timer.minutes,
            seconds: chronometer.timer.seconds,
            secondTenths: chronometer.timer.milliseconds
        }
    });

    timersDictionary[chronometer.id] = eTimer;

    RefreshTimerValues(timersDictionary[chronometer.id], chronometer.id);

    if (chronometer.isRunning == true) {
        timersDictionary[chronometer.id].start({
            precision: 'secondTenths'
        });
        let element = document.querySelector(`#main-button-id${chronometer.id}`);
        element.classList.remove('start-button');
        element.classList.add('stop-button');
    }
}

function RemoveTimer(element) {
    fetch(route + '/' + element.dataset.cid, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    });
}

function RemoveTimerUI(id) {
    timersDictionary[id].stop();
    delete timersDictionary[id];
    let element = document.querySelector(`#remove-button-id${id}`);
    element.parentNode.remove();
}

window.onload = async function GetChronometers() {
    let chronometerData = await fetch(route, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    let data = await chronometerData.json();

    data.forEach((x) => {
        AddChronometerUI(x);
    });
}

connection.on("Add", (model) => {
    let messagesUl = document.querySelector('.messages-ul');
    let newEl = document.createElement('li');
    newEl.innerHTML = `Added timer:  ${model.id}`;
    messagesUl.appendChild(newEl);

    AddChronometerUI(model);
})

connection.on("Update", (model) => {
    let messagesUl = document.querySelector('.messages-ul');
    let newEl = document.createElement('li');
    newEl.innerHTML = `Updated timer:  ${model.id}, Running:  ${model.isRunning}`;
    messagesUl.appendChild(newEl);
    
    UpdateTimerState(model.id, model.isRunning, model.timer.minutes, model.timer.seconds, model.timer.milliseconds);
})

connection.on("Delete", (id) => {
    let messagesUl = document.querySelector('.messages-ul');
    let newEl = document.createElement('li');
    newEl.innerHTML = `Deleted timer: ${id}`;
    messagesUl.appendChild(newEl);

    RemoveTimerUI(id);
})