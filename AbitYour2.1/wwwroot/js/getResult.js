$(function () {

    document.getElementById('start_btn').onclick = function (event) {
        const animationDuration = 550;

        const url = document.getElementById('url').value.trim();
        const name = document.getElementById('name').value.trim();
        const score = document.getElementById('score').value.trim();

        if (isEmptyClicked(url, name, score)) {
            $(".alert-danger").show(animationDuration);
            event.preventDefault();
            return false;
        }

        if (checkOnDanger($('#url').attr('class'), $('#name').attr('class'), $('#score').attr('class'))) {
            $(".alert-danger").show(animationDuration);
            event.preventDefault();
            return false;
        }
        else {
            $(".alert-danger").hide(animationDuration);
        }


        addNumOfRequests();
        return true;
    };


    document.getElementById('score').onchange = function () {
        this.value = this.value.trim();

        if (!this.value.length)
            return;

        const regexp = /^\d{3}([.,]\d{1,3})?$/;
        if (regexp.test(this.value)) {
            this.className = this.className.replace(" text-danger", "");
        }
        else if (!/text-danger/.test(this.className)) {
            this.className = this.className + " text-danger";
        }
    };

    document.getElementById('name').onchange = function () {
        this.value = this.value.trim();

        if (!this.value.length)
            return;

        if (getNumOfWords(this.value) === 1 && !(/([^а-яіiїє]|[ыъ])/i.test(this.value))) {
            this.className = this.className.replace(" text-danger", "");
        }
        else if (!/text-danger/.test(this.className)) {
            this.className = this.className + " text-danger";
        }
    };
});

function blockSubmitButton(button, duration) {
    button.disabled = true;
    setTimeout(() => button.disabled = false, duration);
}

function isEmptyClicked() {
    for (let i = 0; i < arguments.length; i++)
        if (!arguments[i].length)
            return true;

    return false;
}

function checkOnDanger() {
    for (let i = 0; i < arguments.length; i++)
        if (arguments[i].indexOf("danger") !== -1)
            return true;

    return false;
}

function getNumOfWords(str) {
    return str.trim().split("[ \n]").length;
}

let progressBarInterval;
let progress = document.getElementById('progress');
let ariaValueNow = progress.attributes.getNamedItem("aria-valuenow") || progrees.createAttribute("aria-valuenow");

function progressBarAnimationStart() {
    const submitButton = document.getElementById('start_btn');
    const blockSubmitDuration = 5000;
    blockSubmitButton(submitButton, blockSubmitDuration);

    progress.style.width = "0%";

    ariaValueNow.value = '0';

    progressBarInterval = setInterval(function () {
        if (+ariaValueNow.value < 95) {
            ariaValueNow.value = +ariaValueNow.value + 0.5;
            progress.style.width = String(ariaValueNow.value + "%");
        }
    }, 30);
}

function progressBarAnimationStop() {
    clearInterval(progressBarInterval);

    ariaValueNow.value = "100";
    progress.style.width = ariaValueNow.value + "%";
}