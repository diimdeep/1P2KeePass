#!/usr/bin/env bash
grep "AssemblyVersion" 1P2KeePass/Properties/AssemblyInfo.cs | awk -F'"' '/AssemblyVersion/{split($2,a,".");print a[1]"."a[2]"."a[3]}'