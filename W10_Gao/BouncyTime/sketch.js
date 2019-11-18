
let video;
let poseNet;
let poses = [];
let singlePose;
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
  detectionType: 'multiple',
  multiplier: 0.75,
}
let mySound1, mySound2;
let volumeValue = 0.2;

let ring1, ring2, dress;
let ring_sprite, dress_sprite;

let dressCreated = false;
let sound2Played = false;

let leftShoulderX, leftShoulderY, rightShoulderX, rightShoulderY;
let startdance = false;

var h = 0;//the height of the moving dress



function setup() {
  createCanvas(640, 480);
  // Video
  video = createCapture(VIDEO);
  video.size(width, height);
  video.hide();

  //load sound
  mySound1 = loadSound('assets/music1.ogg', loaded);
  mySound2 = loadSound('assets/music2.ogg');

  // Handle PoseNet
  poseNet = ml5.poseNet(video, options, modelReady);
  poseNet.on('pose', function(results) {
    if (getFrame) {
      poses = results;
    }
    getFrame = !getFrame;
  });

  //sprite
  ring1 = loadImage('assets/ring.png');
  ring2 = loadImage('assets/ring2.png');
  dress = loadImage('assets/dress.png');
  ring_sprite = createSprite(220,25); //x, y position, x, y dimension (optional)
  ring_sprite_2 = createSprite(420,25);
  ring_sprite.addImage(ring1);
  ring_sprite_2.addImage(ring1);

}

function loaded() {
  mySound1.loop();
}

function modelReady() {
  //select('#status').style('display: none');
  console.log('Model ready');
}

function draw() {
    background(backgroundColor);
    drawVideo();
    singlePose = poses[0];
    drawCustomPoints(poses);
    mySound1.setVolume(volumeValue);

    drawSprites();

    if(startdance){
      drawPalmmy();
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
  //let singlePose = poses[0];
  if (singlePose) {
    drawTextAtPoint(singlePose.pose.leftWrist, '', 50);
    drawTextAtPoint(singlePose.pose.rightWrist, '', 50);
    volumeValue = singlePose.pose.leftWrist.y / height;
    volumeValue = 1 - volumeValue;

    if(volumeValue > 0.5){
      ring_sprite.addImage(ring2);
      ring_sprite_2.addImage(ring2);
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
    // dress_sprite = createSprite(270,0);
    // dress_sprite.addImage(dress);
    targetDressHeight = singlePose.pose.rightShoulder.y;

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

    else if(!sound2Played && h >= targetDressHeight){
      console.log('dress into position');
      removeSprite(ring_sprite);
      removeSprite(ring_sprite_2);
      mySound2.loop();
      mySound1.stop();
      gotPoses(poses);
      sound2Played = true;
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
