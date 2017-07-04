# GetScreenshot
Get screenshot with .net and win32API

Not long ago, I need a screenshot function in my project.Then I googled some example code and open source code. 
But there are some problems when set scale text font size or change DPI in OS with multi screens.
After all, I decide to write it by myself to solve the problems.

This demo uses Grahics.CopyFromScreen to get screenshot and focus on how to calculate the bound.
I get screen resolution from display setting to calculate the bound and in this way I can ignore all the scale setting in OS.

Requirment:
Windows OS
.Net Framework 4.5
