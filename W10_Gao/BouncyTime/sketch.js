
let video;
let playButton, resetButton;
let poseNet;
let poses = [];
let getFrame = true;
let backgroundColor = 'rgba(255, 201, 207, 0.05)';
let options = {
  imageScaleFactor: 0.3,
  outputStride: 16,
  flipHorizontal: true,
  minConfidence: 0.5,
  maxPoseDetections: 1,
  scoreThreshold: 0.5,
  nmsRadius: 20,
  detectionType: 'single',
  multiplier: 0.75,
}
let mySound1, mySound2, currentSong;
let volumeValue = 0.2;

let shortRing, longRing, dress;
let leftRingSprite, rightRingSprite, dress_sprite;

let dressCreated = false;
let startdance = false;

let leftShoulderX, leftShoulderY, rightShoulderX, rightShoulderY;

let targetDressHeight = 0;
let h = 0;//the height of the moving dress

function preload(){
  //preload sound
  mySound1 = loadSound('assets/music1.ogg');
  mySound2 = loadSound('assets/music2.ogg');
  //preload images
  shortRing = loadImage('assets/ring.png');
  longRing = loadImage('assets/ring2.png');//this will be a sprite sheet
  dress = loadImage('assets/dress.png');//this will be a sprite sheet
  //spot for dress sprite bouncy animation

}

function setup() {
  createCanvas(640, 480);
  // Video
  video = createCapture(VIDEO);
  video.size(width, height);
  video.hide();
  //assign sound
  currentSong = mySound1; // remember to change currentSong when sound2 is playing

  // Handle PoseNet
  poseNet = ml5.poseNet(video, options, modelReady);
  poseNet.on('pose', function(results) {
    if (getFrame) {
      poses = results;
    }
    getFrame = !getFrame;
  });

  //draw sprite
  leftRingSprite = createSprite(220,25);
  rightRingSprite = createSprite(420,25);
  leftRingSprite.addImage(shortRing); // shortRing is a reference to ring1 sprite
  rightRingSprite.addImage(shortRing)

  //create button
  playbutton = createButton('Play/Pause');
  playbutton.mousePressed(togglePlaying);

}

function togglePlaying(){
  if(!currentSong.isPlaying())
  {
    currentSong.play();
    //playButton.html('Pause');
  }else{
    currentSong.pause();
    //playButton.html('Play');
  }

}

function modelReady() {
  //select('#status').style('display: none');
  console.log('Model is ready');
}

function draw() {
    background(backgroundColor);
    drawVideo();
    drawCustomPoints(poses);
    mySound1.setVolume(volumeValue);
    drawSprites();
}

function drawVideo() {
    push();
    translate(width, 0);
    scale(-1, 1);
    image(video, 0, 0, width, height);
    pop();
  }

function drawCustomPoints(poses) {
  if (poses[0]) {
    drawTextAtPoint(poses[0].pose.leftWrist, '🥚', 50);
    drawTextAtPoint(poses[0].pose.rightWrist, '🥚', 50);
    volumeValue = poses[0].pose.leftWrist.y / height;
    volumeValue = 1 - volumeValue;

    if(volumeValue > 0.5){
      leftRingSprite.addImage(longRing);
      rightRingSprite.addImage(longRing);
    }

    if(volumeValue > 0.75){
        console.log('ready to move dress');
        if(!dressCreated) drawDress();
        moveDressDown();

        //stop looping music
      }
    }
  }

function drawTextAtPoint(point, theText, size) {
      if (point.confidence > 0.2) {
        textSize(size);
        textAlign(CENTER);
        fill(240, 240, 240);
        noStroke();
        text(theText, point.x, point.y);
      }
}

function drawDress(){
    dressCreated = true;
    dress_sprite = createSprite(270,0);
    dress_sprite.addImage(dress);
    targetDressHeight = poses[0].pose.rightShoulder.y;

}


function moveDressDown(){

    if(h < targetDressHeight){
      h++;
      console.log(h);
      push();
      translate(270,h);
      noStroke();
      rect(0,0,100,200);
      pop();
    }

    else if(!mySound2.isPlaying() && h >= targetDressHeight){
      console.log('dress into position');
      removeSprite(leftRingSprite);
      removeSprite(rightRingSprite);
      mySound2.loop();
      currentSong = mySound2;
      mySound1.stop();
      gotPoses(poses);
    }
}

function gotPoses(poses){
  //console.log(poses);
  if(poses.length > 0){

    let lsX = poses[0].pose.keypoints[5].position.x;
    let lsY = poses[0].pose.keypoints[5].position.y;
    leftShoulderX = lerp(leftShoulderX, lsX, 0.5);
    leftShoulderY = lerp(leftShoulderY, lsY, 0.5);

    let rsX = poses[0].pose.keypoints[6].position.x;
    let rsY = poses[0].pose.keypoints[6].position.y;
    rightShoulderX = lerp(rightShoulderX, rsX, 0.5);
    rightShoulderY = lerp(rightShoulderY, rsY, 0.5);

  }
  startdance = true;
}

  function drawPalmmy(){
  let diameter = dist(leftShoulderX, leftShoulderY, rightShoulderX, rightShoulderY);
  ellipse((leftShoulderX+rightShoulderX)/2, (leftShoulderY+rightShoulderY)/2, diameter);
  // dress_sprite = createSprite(270,0);
  // dress_sprite.addImage(dress);
  imageMode(CENTER);
  //image(palmmy, leftShoulderX+100, leftShoulderY+100, 320*diameter*0.018, 290*diameter*0.018);

  // dress_sprite.position(leftShoulderX, leftShoulderY);
  // dress_sprite.size(diameter*2, diameter*2);

}
