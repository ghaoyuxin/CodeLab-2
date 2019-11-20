
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

let shortRing, longRing;
let leftRingSprite, rightRingSprite, dress_sprite, hoop_sprite;

let dressCreated = false;
let song2playing = false;
let gameStart = false;
let longRingCreated = false;
let wristOn = false;
let dressReadyToWear = false;
let dressIsWearing = false;

let lsX, lsY, rsX, rsY;
let leftShoulderX, leftShoulderY, rightShoulderX, rightShoulderY;
let shoulderYLastFrame;

let targetDressHeight = 0;
let h = 0;//the height of the moving dress
let tBouncing = 0;

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

  hoop_sprite = createSprite(width/2, 0);
  hoop_sprite.visible = false;
  hoop_sprite.addAnimation('default','assets/hoop_00.png', 'assets/hoop_01.png', 'assets/hoop_02.png', 'assets/hoop_03.png');


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
    mySound1.setVolume(volumeValue);
    drawSprites();
    if(gameStart)
    {
      drawCustomPoints(poses);
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

    if(!longRingCreated && volumeValue > 0.5){
      leftRingSprite.addImage(longRing);
      rightRingSprite.addImage(longRing);
      longRingCreated = true;
    }

    if(dressCreated && volumeValue <= 0.75)
    {
      wristOn = false;
      moveDressDown();
    }

    if(volumeValue > 0.75){
        console.log('ready to move dress');
        if(!dressCreated) drawDress();
        wristOn = true;
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
    hoop_sprite.visible = true;
    dress_sprite.changeAnimation('falling');
    targetDressHeight = poses[0].pose.rightShoulder.y;
}


function moveDressDown(){
    if(h < targetDressHeight && wristOn)
    {
        dress_sprite.setSpeed(1,90);//speed = 1 px, degree = 90
        hoop_sprite.setSpeed(1,90);
    }

    if(h < targetDressHeight && !wristOn){
      dress_sprite.setSpeed(0);
      hoop_sprite.setSpeed(0);
    }

    h = dress_sprite.position.y + 75;//h increments, 75 as manuel offset
    // console.log(h);
    // console.log(targetDressHeight);

    if(!dressIsWearing && h >= targetDressHeight){
      console.log('dress ready to wear');
      //dress to wear
      dressIsWearing = true;
    }
    if(dressIsWearing)
    {
      transitionToWearDress();
      dressFollowPlayer();
    }
}

function transitionToWearDress(){ //function to chop my code into smaller blocks
    //lerp rings out of frame
    let t = 0;
    t++;
    if(t>300)
    {
      removeSprite(leftRingSprite);
      removeSprite(rightRingSprite);
      removeSprite(hoop_sprite);
    }
    //sprite
    leftRingSprite.setSpeed(1, -90);
    rightRingSprite.setSpeed(1, -90);
    hoop_sprite.setSpeed(2,-90);
    //sound
    if(!song2playing)
    {
      //sound
      mySound2.loop();
      currentSong = mySound2;
      mySound1.stop();
      song2playing = true;
    }
  }

function dressFollowPlayer()
  {
    if(!dressReadyToWear){
      dress_sprite.setSpeed(0);
      dress_sprite.changeAnimation('idle');
      dressReadyToWear = true;
    }
    gotPoses();
    dress_sprite.position.x = (lsX+rsX)/2;
    dress_sprite.position.y = (lsY+rsY -120)/2 ;


    let diameter = dist(lsX, lsY, rsX, rsY);
    dress_sprite.scale = diameter*0.01;

    //if lsY+rsY/2
    console.log(abs(shoulderYLastFrame - dress_sprite.position.y));
    if(tBouncing <1 && abs(shoulderYLastFrame - dress_sprite.position.y) > 0.05* height)
    {
      tBouncing = 1;


    }
    if(tBouncing > 0 && tBouncing < 25){
      dress_sprite.changeAnimation('bouncing');
      tBouncing ++;
    }
    if(tBouncing >= 25){
      tBouncing = 0;
      dress_sprite.changeAnimation('idle');
    }
    shoulderYLastFrame = (lsY+rsY -120)/2;
  }


function gotPoses(){
    console.log('Got Poses');
    if(poses.length > 0){

      lsX = poses[0].pose.leftShoulder.x;
      lsY = poses[0].pose.leftShoulder.y;
      // leftShoulderX = lerp(leftShoulderX, lsX, 0.5);
      // leftShoulderY = lerp(leftShoulderY, lsY, 0.5);

      rsX = poses[0].pose.rightShoulder.x;
      rsY = poses[0].pose.rightShoulder.y;
      // rightShoulderX = lerp(rightShoulderX, rsX, 0.5);
      // rightShoulderY = lerp(rightShoulderY, rsY, 0.5);
    }



}
