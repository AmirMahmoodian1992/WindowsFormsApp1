// Establish a WebSocket connection
const socket = new WebSocket('ws://your-server-address:your-port');

// Handle WebSocket messages
socket.addEventListener('message', (event) => {
    const message = JSON.parse(event.data);

    if (message.type === 'callAnswered') {
        // Call answered, start handling WebRTC
        handleWebRTC();
    }
});

// Function to handle WebRTC on the client side
function handleWebRTC() {
    // Request user's microphone access
    navigator.mediaDevices.getUserMedia({ audio: true })
        .then((stream) => {
            const audioContext = new AudioContext();
            const microphone = audioContext.createMediaStreamSource(stream);

            // Connect the microphone stream to the audio context
            microphone.connect(audioContext.destination);

            // Create an AudioProcessorNode to process audio data
            const audioProcessor = audioContext.createScriptProcessor(1024, 1, 1);
            audioProcessor.onaudioprocess = (event) => {
                const inputData = event.inputBuffer.getChannelData(0);

                // Send the audio data to the server using WebSocket
                socket.send(JSON.stringify({
                    type: 'audioData',
                    data: inputData
                }));
            };

            // Connect the audio processor to the microphone and audio context
            microphone.connect(audioProcessor);
            audioProcessor.connect(audioContext.destination);
        })
        .catch((error) => {
            console.error('Error accessing microphone:', error);
        });
}
