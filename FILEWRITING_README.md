# Writing Player Actions to File System:
 
 As the game is played, the actions committed by the player are stored to a file in Application.persistentDataPath. The file is a text file where every line is an individual action, and information regarding the elements is space separated. Currently, it will record the player’s current session number, the time the action occurred, and what type of action it was, including player login, mouse clicks, keyboard key presses, keyboard key releases, button presses, scene changes, shape rotations, and challenge submissions.

## Line Format:

#### SessionNumber Year Month Day Hour Minute Second Millisecond ActionType …ActionDetails… 

## Action Types and Details:


### Login

#### Login

Records when the player enters a username and logs in to the game

&nbsp;

### Click

#### Click mouseLocationX mouseLocationY mouseLocationZ
   
mouseLocationX: X coordinate of the mouse 

mouseLocationY: Y coordinate of the mouse

mouseLocationZ: Z coordinate of the mouse

Whenever the user clicks, records the location of the mouse.

&nbsp;

### Scene Change

#### SceneChange oldSceneName newSceneName

oldSceneName: name of the previous scene 

newSceneName: name of the current, new scene

Whenever the game scene changes, record the transition between scenes.

&nbsp;

### Challenge Submission

#### ChallengeSubmission challengeName success currentChallengeNum totalChallenges

challengeName: name of the current challenge

success: whether the user answered correctly or not, True or False 

currentChallengeNum: the current question number that the user is on 

totalChallenges: the total number of questions for the user in this set

Whenever the user submits an answer to a challenge question it records this information. 

&nbsp;

### Button Press

#### ButtonPress buttonName

When the user clicks an in-game button, it records the name of the button pressed.

&nbsp;

### Keyboard Key Press

#### KeyPress keyName

keyName: symbol/letter of the key pressed

When the user presses a keyboard key, it records which key was pressed. 

&nbsp;

### Keyboard Key Release

#### KeyReleased keyName

keyName: symbol/letter of the key released

When the user releases a keyboard key, it records which key was released.

&nbsp;

### Screen Size

#### ScreenSize screenWidth screenHeight

screenWidth: the current width of the user's screen in pixels

screenHeight: the current height of the user's screen in pixels

Records the user's screen size in pixels to be used for the location of mouse clicks relative to the game's field. Records at start and if it changes.

&nbsp;

### Object Rotation

#### ObjectRotation objectName pastX pastY pastZ newX newY newZ

objectName: the name of the object being rotated

pastX: the previous Euler X rotation value before the rotation 

pastY: the previous Euler Y rotation value before the rotation 

pastZ: the previous Euler Z rotation value before the rotation 

newX: the current Euler X rotation value after the rotation 

newY: the current Euler Y rotation value after the rotation 

newZ: the current Euler Z rotation value after the rotation

When the user rotates the in-game object/shape, the change in rotation values is recorded. 

####This is currently inconsistent and analysis should currently be done using key, mouse, and button presses.
