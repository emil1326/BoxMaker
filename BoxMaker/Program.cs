using BoxMaker.core;

// https://h-deb.ca/CLG/Cours/420KBK/boites-v0.html

string text = "Hello, World!";
Console.WriteLine(Box(text,0));
Console.WriteLine(ComboHorizontal(1, Box(text, 5), Box("a box", 2)));

Box b = new ();
Console.WriteLine(b);
Console.WriteLine(new Box("yo"));
string texte = @"Man! Hey!!!
ceci est un test
multiligne";
string autTexte = "Ceci\nitou, genre";
Box b0 = new (texte);
Box b1 = new (autTexte);
Console.WriteLine(b0);
Console.WriteLine(b1);
ComboVertical cv = new (b0, b1);
Console.WriteLine(new Box(cv));
ComboHorizontal ch = new (b0, b1);
Console.WriteLine(new Box(ch));
ComboVertical cvplus = new (new Box(cv), new Box(ch));
Console.WriteLine(new Box(cvplus));
ComboHorizontal chplus = new (new Box(cv), new Box(ch));
Console.WriteLine(new Box(chplus));
// ComboVertical cvv = new (new Boite(chplus), new Boite("coucou"));
// Console.WriteLine(new Boite(cvv));
// Console.WriteLine(new Boite(
//    new ComboHorizontal(
//       new Boite("a\nb\nc\nd\ne"),
//          new Boite(
//             new ComboVertical(
//                new Boite("allo"), new Boite("yo")
//             )
//          )
//       )
//    )
// );
// Console.WriteLine(
//    new Boite(new ComboHorizontal(new Boite("Yo"), new Boite()))
// );
// Console.WriteLine(
//    new Boite(new ComboHorizontal(new Boite(), new Boite("Ya")))
// );
// Console.WriteLine(
//    new Boite(new ComboHorizontal(new Boite(), new Boite()))
// );
// Console.WriteLine(
//    new Boite(new ComboVertical(new Boite(), new Boite()))
// );
// Console.WriteLine(
//    new Boite(new ComboVertical(new Boite("Yip"), new Boite()))
// );
// Console.WriteLine(
//    new Boite(new ComboVertical(new Boite(), new Boite("Yap")))
// );


Console.ReadLine();