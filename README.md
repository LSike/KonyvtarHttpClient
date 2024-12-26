A program egy WPF kliens, ami egy ASP.NET API backend végpontjaihoz kapcsolódik.
A backend XAMPP mysql adatbázishoz kapcsolódik a localhoston, így a XAMPP fusson. :-)
A backendet az exe-vel indítom, így a http://localhost:5000 címet használja
A használt végpontok hasonlóak, alapvetően a http://localhost:5000/Konyvtarak címen érhetők el, a metódus dönt, hogy mi történik:

GET --> Konyvtarak/GetDTO
DELETE --> Konyvtarak (az id egész értéket paraméterként adom át)
POST --> Konyvtarak (a body tartalmazza az objektumot)
PUT --> Konyvtarak (a body tartalmazza az objektumot)
