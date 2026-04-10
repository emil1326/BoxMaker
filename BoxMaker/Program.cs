using Boites;
using Boite = Boites.Box;
using Box = Boites.Box;
using System.IO;

// If a .test file is passed as first argument, run the standardized test-loop and exit.
if (args.Length > 0)
{
   var coul = new Couleureur();
   var mes = new Mesureur();
   using (var sr = new StreamReader(args[0]))
      for (var s = LireBoite(sr); s != null; s = LireBoite(sr))
         Tester(FabriqueBoites.Créer(s), coul, mes);

   static string LireBoite(StreamReader sr)
   {
      string s = sr.ReadLine();
      while (s != null && s.Trim().Length == 0)
         s = sr.ReadLine();
      if (s != null)
         for (string ligne = sr.ReadLine(); ligne != null && ligne.Trim().Length != 0; ligne = sr.ReadLine())
            s += $"\n{ligne}";
      return s;
   }

   static void Tester(Box b, params IVisiteur<Box>[] viz)
   {
      Console.Write(b);
      foreach (var v in viz)
         b.Accepter(v);
   }

   return;
}

// https://h-deb.ca/CLG/Cours/420KBK/boites-v0.html

Boite b = new ();
Console.WriteLine(b);
Console.WriteLine(new Boite("yo"));
string texte = @"Man! Hey!!!
ceci est un test
multiligne";
string autTexte = "Ceci\nitou, genre";
Boite b0 = new (texte);
Boite b1 = new (autTexte);
Console.WriteLine(b0);
Console.WriteLine(b1);
ComboVertical cv = new (b0, b1);
Console.WriteLine(new Boite(cv));
ComboHorizontal ch = new (b0, b1);
Console.WriteLine(new Boite(ch));
ComboVertical cvplus = new (new Boite(cv), new Boite(ch));
Console.WriteLine(new Boite(cvplus));
ComboHorizontal chplus = new (new Boite(cv), new Boite(ch));
Console.WriteLine(new Boite(chplus));
ComboVertical cvv = new (new Boite(chplus), new Boite("coucou"));
Console.WriteLine(new Boite(cvv));
Console.WriteLine(new Boite(
    new ComboHorizontal(
       new Boite("a\nb\nc\nd\ne"),
          new Boite(
             new ComboVertical(
                new Boite("allo"), new Boite("yo")
                )
            )
        )
    )
);
Console.WriteLine(
   new Boite(new ComboHorizontal(new Boite("Yo"), new Boite()))
);
Console.WriteLine(
   new Boite(new ComboHorizontal(new Boite(), new Boite("Ya")))
);
Console.WriteLine(
   new Boite(new ComboHorizontal(new Boite(), new Boite()))
);
Console.WriteLine(
   new Boite(new ComboVertical(new Boite(), new Boite()))
);
Console.WriteLine(
   new Boite(new ComboVertical(new Boite("Yip"), new Boite()))
);
Console.WriteLine(
   new Boite(new ComboVertical(new Boite(), new Boite("Yap")))
);

TestFabriques();

TesterVisiteurs();

Console.ReadLine();

static void TestFabriques()
{
   Box p = FabriqueBoites.Créer("mono J'aime mon \"prof\"");
   Console.WriteLine(new Boite(p));
   p = FabriqueBoites.Créer("cv\nmono J'aime mon \"prof\"\nmono moi itou");
   Console.WriteLine(new Boite(p));
   p = FabriqueBoites.Créer("ch\nmono J'aime mon \"prof\"\nmono moi itou");
   Console.WriteLine(new Boite(p));
   p = FabriqueBoites.Créer(
      "ch\ncv\nmono J'aime mon \"prof\"\nmono moi itou\nmono eh ben");
   Console.WriteLine(new Boite(p));
   p = FabriqueBoites.Créer(
      "ch\ncv\nmc\nmono J'aime mon \"prof\"\nmono moi itou\nmono eh ben");
   Console.WriteLine(new Boite(p));
}

static void TesterVisiteurs()
{
   static void Tester(Boite b, params IVisiteur<Box>[] viz)
   {
      Console.WriteLine(b);
      foreach (IVisiteur<Box> v in viz)
         b.Accepter(v);
   }
   Couleureur coul = new Couleureur();
   Mesureur mes = new Mesureur();
   Tester(new Boite(), coul, mes);
   Tester(new Boite("yo"), coul, mes);
   string texte = @"Man! Hey!!!
ceci est un test
multiligne";
   string autTexte = "Ceci\nitou, genre";
   Boite b0 = new Boite(texte);
   Boite b1 = new Boite(autTexte);
   Tester(b0, coul, mes);
   Tester(b1, coul, mes);
   ComboVertical cv = new ComboVertical(b0, b1);
   Tester(new Boite(cv), coul, mes);
   ComboHorizontal ch = new ComboHorizontal(b0, b1);
   Tester(new Boite(ch), coul, mes);
   ComboVertical cvplus = new ComboVertical(new Boite(cv), new Boite(ch));
   Tester(new Boite(cvplus), coul, mes);
   ComboHorizontal chplus = new ComboHorizontal(new Boite(cv), new Boite(ch));
   Tester(new Boite(chplus), coul, mes);
   ComboVertical cvv = new ComboVertical(new Boite(chplus), new Boite("coucou"));
   Tester(new Boite(cvv), coul, mes);
   Tester(new Boite(
      new ComboHorizontal(
         new Boite("a\nb\nc\nd\ne"),
            new Boite(
               new ComboVertical(
                  new Boite("allo"), new Boite("yo")
               )
            )
         )
      ), coul, mes
   );
   Tester(
      new Boite(new ComboHorizontal(new Boite("Yo"), new Boite())),
      coul, mes
   );
   Tester(
      new Boite(new ComboHorizontal(new Boite(), new Boite("Ya"))),
      coul, mes
   );
   Tester(
      new Boite(new ComboHorizontal(new Boite(), new Boite())),
      coul, mes
   );
   Tester(
      new Boite(new ComboVertical(new Boite("Yip"), new Boite())),
      coul, mes
   );
   Tester(
      new Boite(new ComboVertical(new Boite(), new Boite("Yap"))),
      coul, mes
   );
   Tester(
      new Boite(new ComboVertical(new Boite(), new Boite())),
      coul, mes
   );
   Tester(new Boite(new MonoCombo(new Boite("allo"))), coul, mes);
   Tester(new Boite(
      new MonoCombo(new Boite(new MonoCombo(new Boite("allo"))))
   ), coul, mes);
   Tester(new Boite(
      new ComboVertical(
         new Boite(new MonoCombo(new Boite(new MonoCombo(new Boite("allo"))))),
         new Boite("Eh ben")
      )
   ), coul, mes);
   Tester(new Boite(
      new ComboHorizontal(new Boite("a\nb\nc\nd"),
                          new Boite(new MonoCombo(new Boite())))
   ), coul, mes);
   Tester(new Boite(
      new ComboHorizontal(new Boite(),
                          new Boite(new MonoCombo(new Boite())))
   ), coul, mes);
   Tester(new Boite(
      new ComboHorizontal(
         new Boite(new MonoCombo(new Boite(new MonoCombo(new Boite("allo"))))),
         new Boite(new ComboVertical(
            new Boite("Eh ben"),
            new Boite(new MonoCombo(new Boite(
               new ComboHorizontal(new Boite("yo"), new Boite("hey"))
            )))
         ))
      )
   ), coul, mes);
   Console.WriteLine($"\n\nLa plus petite boite est :\n{mes.PlusPetite}");
   Console.WriteLine($"\n\nLa plus grande boite est :\n{mes.PlusGrande}");
}