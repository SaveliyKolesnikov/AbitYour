window.addEventListener("resize", setBottomPadding);
setBottomPadding();

function setBottomPadding() {
    const mainWrapper = document.getElementById("main-wrapper");
    const footer = document.getElementById("navigate-footer");
    mainWrapper.style.paddingBottom = footer.offsetHeight + "px";
}
