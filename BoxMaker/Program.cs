using BoxMaker.core;

// https://h-deb.ca/CLG/Cours/420KBK/boites-v0.html

string text = "Hello, World!";
Console.WriteLine(Box(text,1));
Console.WriteLine(ComboHorizontal(1, Box(text, 1), Box("Another box", 2)));

// Boite b = new ();
// Console.WriteLine(b);
// Console.WriteLine(new Boite("yo"));
// string texte = @"Man! Hey!!!
// ceci est un test
// multiligne";
// string autTexte = "Ceci\nitou, genre";
// Boite b0 = new (texte);
// Boite b1 = new (autTexte);
// Console.WriteLine(b0);
// Console.WriteLine(b1);
// ComboVertical cv = new (b0, b1);
// Console.WriteLine(new Boite(cv));
// ComboHorizontal ch = new (b0, b1);
// Console.WriteLine(new Boite(ch));
// ComboVertical cvplus = new (new Boite(cv), new Boite(ch));
// Console.WriteLine(new Boite(cvplus));
// ComboHorizontal chplus = new (new Boite(cv), new Boite(ch));
// Console.WriteLine(new Boite(chplus));
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