﻿# Runs the first time a package is installed in a solution, and every time the solution is opened.

param($installPath, $toolsPath, $package, $project)

# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is null in init.ps1

$latest = (Get-ItemProperty 'hkcu:Software\IronMeta\VSPackage' -ErrorAction SilentlyContinue).Latest

if ($latest -ne $package.version) {
	iex ((join-path $installPath "\tools\net45\IronMeta.VSPackage.exe") + " " + (join-path $installPath "\lib\net45\IronMeta.VSPlugin.dll") + " " + (join-path $installPath "\lib\net45\IronMeta.Generator.dll") + " " + (join-path $installPath "\lib\net45\IronMeta.Matcher.dll"))

	New-Item -Path 'hkcu:Software\IronMeta' -Name VSPackage -Force
	Set-ItemProperty 'hkcu:Software\IronMeta\VSPackage' -Name Latest -Value $package.version
}