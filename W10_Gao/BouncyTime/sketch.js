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
//- add package Atom live server -test
//download Web Server for Chrome, a chrome extension allow you to host your project
//select your project foler, now click on the URL to open my webpage

//STEP4: connect p5.play with my project
//-copy p5.play.js	script into libraries folder - add this line to index.html <script src="libraries/p5.play.js" type="text/javascript"></script>
//-test by drawing a sprite

function setup() {
  createCanvas(800,400);
  createSprite(400, 200, 50, 50);
}

function draw() {
  background(255,255,255);
  drawSprites();
}
