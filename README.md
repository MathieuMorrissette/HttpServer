I included some sample websites


To add new website use a sym link in the websites folder

# Example

in the /git/ folder
```
git clone https://github.com/MathieuMorrissette/MathMoHTTP.git
git clone https://github.com/MathieuMorrissette/ExaltedWorld.git
```

Then create a symlink like so :
```
mklink /J "MathMoHTTP\HttpServer\websites\exalted\" "ExaltedWorld\src"
```

