const WINDOW_ID = 'my-glass-window';
function handleButtonClick() {
    const url = "yoump3:" + window.location.href;
    window.open(url); 
}

function createFloatingWindow() {
    const windowDiv = document.createElement('div');
    windowDiv.id = WINDOW_ID;
    windowDiv.innerHTML = `
        <button id="my-glass-close-button" aria-label="Chiudi finestra temporaneamente">×</button> 
        <div id="my-glass-window-title">YouMP3</div>
        <button id="my-glass-action-button">
            ${getDownloadSvg()}
            Scarica
        </button>
    `;
    return windowDiv;
}

function getDownloadSvg() {
    return `
        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" viewBox="0 0 16 16">
          <path fill-rule="evenodd" d="M7.646 10.854a.5.5 0 0 0 .708 0l2-2a.5.5 0 0 0-.708-.708L8.5 9.293V5.5a.5.5 0 0 0-1 0v3.793L6.354 8.146a.5.5 0 1 0-.708.708z"/>
          <path d="M4.406 3.342A5.53 5.53 0 0 1 8 2c2.69 0 4.923 2 5.166 4.579C14.758 6.804 16 8.137 16 9.773 16 11.569 14.502 13 12.687 13H3.781C1.708 13 0 11.366 0 9.318c0-1.763 1.266-3.223 2.942-3.593.143-.863.698-1.723 1.464-2.383m.653.757c-.757.653-1.153 1.44-1.153 2.056v.448l-.445.049C2.064 6.805 1 7.952 1 9.318 1 10.785 2.23 12 3.781 12h8.906C13.98 12 15 10.988 15 9.773c0-1.216-1.02-2.228-2.313-2.228h-.5v-.5C12.188 4.825 10.328 3 8 3a4.53 4.53 0 0 0-2.941 1.1z"/>
        </svg>
    `;
}

function injectFloatingWindow() {
    if (!document.body || (!window.location.host.includes('youtube.com') && !window.location.host.includes('music.youtube.com'))) {
        return; 
    }

    if (document.getElementById(WINDOW_ID)) {
        return;
    }

    const floatingWindow = createFloatingWindow();
    document.body.appendChild(floatingWindow);

    const actionButton = document.getElementById('my-glass-action-button');
    if (actionButton) {
        actionButton.addEventListener('click', handleButtonClick);
    }
    
    const closeButton = document.getElementById('my-glass-close-button');
    if (closeButton) {
        closeButton.addEventListener('click', () => {
            const glassWindow = document.getElementById(WINDOW_ID);
            if (glassWindow) {
                glassWindow.style.display = 'none';
            }
        });
    }
}

injectFloatingWindow(); 
setInterval(injectFloatingWindow, 1000); 