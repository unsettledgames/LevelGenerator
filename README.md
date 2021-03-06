<h1>LevelGenerator</h1>

The LevelGenerator is a C# program that generates levels (in bitmap format) composed by rectangular rooms connected by corridors. 
It is perfect for 2D top-down games, but it can also be used to build levels for sidescroller games.

<center><img src = "https://i.redd.it/vxa35wy8zl131.png"/></center>

<h2>How does it work?</h2>
<ul>
<li>The program starts by generating a single room. </li>
<li>For each side of the room, a corridor will be added (depending on the corridorProbability and if there is enough space), along with a room at its end.</li>
<li>The process basically continues until the number of generated rooms equals nRooms</li>
<li>To give it a little bit more of randomness, the room to which the corridors are added, can change depending on a parameter</li>

<h2>Parameters</h2>

At the moment, it is not possible to send any input to the application; for debug purposes I only made it possible to change the generation 
parameters via code (which is much faster than having to insert the correct parameters all the times).
However, input will be surely implemented. We can probably let the user decide to use fixed params or custom ones, so that we can still debug
fastly.<br><br>
Here are the generation parameters:
<ul>
<li><strong>minRoomWidth:</strong> minimum width of each room</li>
<li><strong>maxRoomWidth:</strong> maximum width of each room</li>
<li><strong>minRoomHeight:</strong> minimum height of each room</li>
<li><strong>maxRoomHeigh:</strong> maximum height of each room</li>

<li><strong>minCorridorWidth:</strong> minimum width of each corridor</li>
<li><strong>maxCorridorWidth:</strong> maximum width of each corridor</li>
<li><strong>minCorridorHeight:</strong> minimum height of each corridor</li>
<li><strong>maxCorridorHeight:</strong> maximum height of each corridor</li>

<li><strong>maxCorridorsPerSide:</strong> maximum number of corridors attached to a single side of the room </li>
<li><strong>corridorProbability:</strong> probability (from 0 to 100) that a corridor is attached to a side of a room</li>
<li><strong>nRooms:</strong> total number of rooms to generate
<li><strong>backgroundColor:</strong> the background color in the resulting bitmap</li>
<li><strong>foregroundColor:</strong> the foreground color in the resulting bitmap</li>
<li><strong>roomChangeProbability:</strong> probability (from 0 to 100) that the program changes the room to attach corridors to

</ul>

<h2>To do:</h2>
<ul>
  <li>Add perlin noise to the borders of the rooms</li>
  <li>Existing bitmap opening (requires to build some kind of format to store data about rooms and corridors)</li>
  <li>Add a GUI (at least to show a preview of the bitmap, but it can be also used to input parameters)</li>
  <li>Have corridors that are not just a straight line from a room to another</li>
  <li>Possibility to add a level starting from a room of an existing level</li>
  <li>Cropping bitmap in case of excessive space</li>
</ul>

<h2>Notes</h2>

<ul>
</ul>

Distributed under the <a href = "https://www.gnu.org/licenses/gpl-3.0.en.html">GPL3 license</a>.
