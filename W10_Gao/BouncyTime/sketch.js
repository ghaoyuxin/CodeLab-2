
let video;
let playbutton, resetbutton;
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
  scoreThreshold: 0.6,
  nmsRadius: 20,
  detectionType: 'single',
  multiplier: 0.75,
}
let mySound1, mySound2, currentSong;
let volumeValue = 0.2;

let shortRing, longRing, dress_sheet;
let leftRingSprite, rightRingSprite, dress_sprite;

let dressCreated = false;
let song2playing = false;
let gameStart = false;

//let lsX, lsY, rsX, rsY;
let leftShoulderX, leftShoulderY, rightShoulderX, rightShoulderY;

let targetDressHeight = 0;
let h = 0;//the height of the moving dress

function preload(){
  //preload sound
  mySound1 = loadSound('assets/music1.ogg');
  mySound2 = loadSound('assets/music2.ogg');
  //preload images
  shortRing = loadImage('assets/ring.png');
  longRing = loadImage('assets/ring2.png');
  //dress_sheet = loadSpriteSheet('assets/dress.png');


}

function setup() {
  createCanvas(640, 480);
  // Video
  video = createCapture(VIDEO);
  video.size(width, height);
  video.hide();
  //assign sound
  currentSong = mySound1; // remember to change currentSong when sound2 is playing
  currentSong.pause();

  // Handle PoseNet
  poseNet = ml5.poseNet(video, options, modelReady);
  poseNet.on('pose', function(results) {
    if (getFrame) {
      poses = results;
    }
    getFrame = !getFrame;
  });

  //draw sprite
  let x = 220; // x coordinate
  leftRingSprite = createSprite(x,25);
  rightRingSprite = createSprite(640-x,25);
  leftRingSprite.visible = false;
  rightRingSprite.visible = false;
  leftRingSprite.addImage(shortRing); // shortRing is a reference to ring1 sprite
  rightRingSprite.addImage(shortRing)

  //animation
  dress_sprite = createSprite(width/2, 0);
  dress_sprite.visible = false;
  dress_sprite.addAnimation('falling', 'assets/dress_00.png', 'assets/dress_01.png', 'assets/dress_02.png', 'assets/dress_03.png');
  dress_sprite.addAnimation('bouncing', 'assets/dress_04.png', 'assets/dress_05.png', 'assets/dress_06.png',  'assets/dress_07.png', 'assets/dress_08.png');
  dress_sprite.addAnimation('idle', 'assets/dress_09.png', 'assets/dress_10.png', 'assets/dress_11.png');


  //create button
  playbutton = createButton('Play');
  playbutton.mousePressed(togglePlaying);

}

function togglePlaying(){
  if(!gameStart){
    gameStart = true;
    leftRingSprite.visible = true;
    rightRingSprite.visible = true;
  }

  if(!currentSong.isPlaying())
  {
    currentSong.play();
    playbutton.html('Pause');
  }else{
    currentSong.pause();
    playbutton.html('Play');
  }

}

function modelReady() {
  //select('#status').style('display: none');
  console.log('Model ready');
}

function draw() {
    background(backgroundColor);
    drawVideo();
    drawSprites();
    if(gameStart)
    {
      drawCustomPoints(poses);
      mySound1.setVolume(volumeValue);
    }
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
    dress_sprite.visible = true;
    dress_sprite.changeAnimation('falling');
    targetDressHeight = poses[0].pose.rightShoulder.y;
}


function moveDressDown(){
    h = dress_sprite.position.y;
    if(h < targetDressHeight){
      console.log(h);
      dress_sprite.setSpeed(1,90);//speed = 1 px, degree = 90

    }

    else if(!song2playing && h >= targetDressHeight){
      console.log('dress into position');
      removeSprite(leftRingSprite);
      removeSprite(rightRingSprite);
      mySound2.loop();
      song2playing = true;
      currentSong = mySound2;
      mySound1.stop();

    }
}

// function gotPoses(poses){
//   //console.log(poses);
//   if(poses.length > 0){
//
//     lsX = poses[0].pose.leftShoulder.position.x;
//     lsY = poses[0].pose.leftShoulder.position.y;
//     leftShoulderX = lerp(leftShoulderX, lsX, 0.5);
//     leftShoulderY = lerp(leftShoulderY, lsY, 0.5);
//
//     rsX = poses[0].pose.rightShoulder.position.x;
//     rsY = poses[0].pose.rightShoulder.position.y;
//     rightShoulderX = lerp(rightShoulderX, rsX, 0.5);
//     rightShoulderY = lerp(rightShoulderY, rsY, 0.5);
//
//   }
//}

  // function drawPalmmy(){
  // let diameter = dist(leftShoulderX, leftShoulderY, rightShoulderX, rightShoulderY);
  // ellipse((leftShoulderX+rightShoulderX)/2, (leftShoulderY+rightShoulderY)/2, diameter);
  // // dress_sprite = createSprite(270,0);
  // // dress_sprite.addImage(dress);
  // imageMode(CENTER);
  // //image(palmmy, leftShoulderX+100, leftShoulderY+100, 320*diameter*0.018, 290*diameter*0.018);
  //
  // // dress_sprite.position(leftShoulderX, leftShoulderY);
  // // dress_sprite.size(diameter*2, diameter*2);

//}
