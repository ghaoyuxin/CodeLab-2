//STEP1: setup a local project folder on my desktop
//install node
//- go to website https://nodejs.org/en/download/ - test by open Terminal and type "node", if it returns ">" then it is working, node will allow me to use npm install p5-manager
//install p5-manager via npm
//-open terminal -"sudo npm install p5-manager -g" -test by type "p5" I should see a welcome message
//great,now setup a project folder on desktop
//-in terminal, nagivate to the directory - "p5 generate -b BouncyTime" //it will generate a folder with a HTML, a sketch.js, a libraries folder

//STEP2: install editor
//Atom
//-test by drawing a green background

//STEP3: host my project on a server
//- add package Atom live server -open live server from packages, test

//STEP4: Complier
//open -chrome -view -developer -javascript console for checking error messages
//settings: disable cache while DevsTool is open

//STEP4: connect p5.play with my project
//-copy p5.play.js	script into libraries folder - add this line to index.html <script src="libraries/p5.play.js" type="text/javascript"></script>
//-test by drawing a sprite

//STEP%: setup postNet library in the project

let video;
let poseNet;
let noseX = 0;
let noseY = 0;
let eyelX = 0;
let eyelY = 0;

function setup() {
  createCanvas(640, 480);
  video = createCapture(VIDEO);
  video.hide();
  poseNet = ml5.poseNet(video, modelReady);
  poseNet.on('pose', gotPoses);
}

function gotPoses(poses) {
  // console.log(poses);
  if (poses.length > 0) {
    let nX = poses[0].pose.keypoints[0].position.x;
    let nY = poses[0].pose.keypoints[0].position.y;
    let eX = poses[0].pose.keypoints[1].position.x;
    let eY = poses[0].pose.keypoints[1].position.y;
    noseX = lerp(noseX, nX, 0.5);
    noseY = lerp(noseY, nY, 0.5);
    eyelX = lerp(eyelX, eX, 0.5);
    eyelY = lerp(eyelY, eY, 0.5);
  }
}

function modelReady() {
  console.log('model ready');
}

function draw() {
  image(video, 0, 0);

  let d = dist(noseX, noseY, eyelX, eyelY);

  fill(255, 0, 0);
  ellipse(noseX, noseY, d);

}
