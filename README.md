I included some sample websites


To add new website use a sym link in the websites folder

# Example

in the /git/ folder
```
git clone https://github.com/MathieuMorrissette/MathMoHTTP.git
git clone https://github.com/MathieuMorrissette/ExaltedWorld.git
git clone https://github.com/MathieuMorrissette/L33T_K3YB04RD.git
```

Then create a symlink like so :
```
mklink /J "MathMoHTTP\HttpServer\websites\exalted\" "ExaltedWorld\src"
mklink "MathMoHTTP\HttpServer\websites\key\public\html\sender.html" "..\..\..\..\..\..\L33T_K3YB04RD\sender.html"
```

