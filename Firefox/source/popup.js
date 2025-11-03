document.getElementById("sendToApp").addEventListener("click", async () => {
  try {
    let tabs = await browser.tabs.query({ active: true, currentWindow: true });
    let url = tabs[0].url;
    if (url.includes("youtube.com")) {
      window.open("yoump3:" + encodeURIComponent(url));
    } else {
      alert("Questo sito non è supportato da YouMP3.");
    }
  } catch (err) {
    console.error(err);
  }
});
