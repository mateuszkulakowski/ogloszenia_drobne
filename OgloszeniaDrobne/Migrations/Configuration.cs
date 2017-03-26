namespace OgloszeniaDrobne.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<OgloszeniaDrobne.DAL.DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "OgloszeniaDrobne.DAL.DbContext";
        }

        protected override void Seed(OgloszeniaDrobne.DAL.DbContext context)
        {

            User u1 = new User { Login = "admin", Email = "admin@admin.pl", Haslo = "admin", Rola = "ADMIN", Imie = "Janusz", Nazwisko = "Polski", IloscOgloszenStrona = 5 };
            User u2 = new User { Login = "user", Email = "user@user.pl", Haslo = "user", Rola = "USER", Imie = "Krzysztof", Nazwisko = "Kononowicz", IloscOgloszenStrona = 5 };
            User u3 = new User { Login = "user2", Email = "mariuszko@user.pl", Haslo = "user2", Rola = "USER", Imie = "Robert", Nazwisko = "Rynkiewicz", IloscOgloszenStrona = 5 };
            User u4 = new User { Login = "user3", Email = "ibisz@user.pl", Haslo = "user3", Rola = "USER", Imie = "Klaudiusz", Nazwisko = "Przyrodnik", IloscOgloszenStrona = 5 };
            User u5 = new User { Login = "user4", Email = "jarosla@user.pl", Haslo = "user4", Rola = "USER", Imie = "Korneliusz", Nazwisko = "Chojnicki", IloscOgloszenStrona = 5 };
            User u6 = new User { Login = "user5", Email = "polskagora@user.pl", Haslo = "user5", Rola = "USER", Imie = "Maciej", Nazwisko = "Nowik", IloscOgloszenStrona = 5 };


            //Category c1 = new Category { Nazwa = "Motoryzacja" };
            //Category c2 = new Category { Nazwa = "Ogród"};
            //Category c3 = new Category { Nazwa = "Dom"};
            //Category c4 = new Category { Nazwa = "Zwierzeta"};
            //Category c5 = new Category { Nazwa = "Elektronika" };
            //Category c6 = new Category { Nazwa = "Sport" };
            //Category c7 = new Category { Nazwa = "Kultura" };
            //Category c8 = new Category { Nazwa = "Moda" };

            ////podkategorie motoryzacja
            //Category c11 = new Category { Nazwa = "Pojazdy", ParentCategory=c1};
            //    Category c111 = new Category { Nazwa = "Osobowe", ParentCategory = c11 };
            //        Category c1111 = new Category { Nazwa = "Seat", ParentCategory = c111 };
            //        Category c1112 = new Category { Nazwa = "Volkswagen", ParentCategory = c111 };
            //        Category c1113 = new Category { Nazwa = "Volvo", ParentCategory = c111 };
            //        Category c1114 = new Category { Nazwa = "Opel", ParentCategory = c111 };
            //    Category c112 = new Category { Nazwa = "Traktory", ParentCategory = c11 };
            //        Category c1121 = new Category { Nazwa = "Wladymirec", ParentCategory = c112 };
            //        Category c1122 = new Category { Nazwa = "Zetor", ParentCategory = c112 };
            //        Category c1123 = new Category { Nazwa = "Ursus", ParentCategory = c112 };
            //Category c12 = new Category { Nazwa = "Motocykle", ParentCategory = c1 };
            //    Category c121 = new Category { Nazwa = "Enduro", ParentCategory = c12 };
            //    Category c122 = new Category { Nazwa = "Turystyczne", ParentCategory = c12 };
            //    Category c123 = new Category { Nazwa = "Sportowe", ParentCategory = c12 };
            //Category c13 = new Category { Nazwa = "Czêœci Motoryzacja", ParentCategory = c1 };
            //    Category c131 = new Category { Nazwa = "Opony", ParentCategory = c13 };
            //    Category c132 = new Category { Nazwa = "Filtry", ParentCategory = c13 };
            //    Category c133 = new Category { Nazwa = "Œwiece", ParentCategory = c13 };

            ////podkategorie ogród
            //Category c21 = new Category { Nazwa = "Kwiaty", ParentCategory = c2 };
            //    Category c211 = new Category { Nazwa = "Dzikie", ParentCategory = c21 };
            //    Category c212 = new Category { Nazwa = "Hodowlane", ParentCategory = c21 };
            //Category c22 = new Category { Nazwa = "Baseny", ParentCategory = c2 };
            //Category c23 = new Category { Nazwa = "Grilowanie", ParentCategory = c2 };
            //    Category c231 = new Category { Nazwa = "Grile", ParentCategory = c23 };
            //    Category c232 = new Category { Nazwa = "Akcesoria Grilowe", ParentCategory = c23 };
            //Category c24 = new Category { Nazwa = "Narzêdzia ogrodnicze", ParentCategory = c2 };

            ////podkategorie dom
            //Category c31 = new Category { Nazwa = "Akcesoria Domowe", ParentCategory = c3 };
            //    Category c311 = new Category { Nazwa = "Krajalnice", ParentCategory = c31 };
            //    Category c312 = new Category { Nazwa = "Chlebaki", ParentCategory = c31 };
            //    Category c313 = new Category { Nazwa = "No¿e", ParentCategory = c31 };
            //Category c32 = new Category { Nazwa = "Œrodki czystoœci", ParentCategory = c3 };
            //Category c33 = new Category { Nazwa = "Garnki", ParentCategory = c3 };
            //Category c34 = new Category { Nazwa = "Meble", ParentCategory = c3 };
            //    Category c341 = new Category { Nazwa = "£azienkowe", ParentCategory = c34 };
            //    Category c342 = new Category { Nazwa = "Kuchenne", ParentCategory = c34 };

            ////podkategorie zwierzêta
            //Category c41 = new Category { Nazwa = "Domowe", ParentCategory = c4 };
            //Category c42 = new Category { Nazwa = "Dzikie", ParentCategory = c4 };
            //Category c43 = new Category { Nazwa = "Hodowlane", ParentCategory = c4 };


            ////podkategorie elektronika
            //Category c51 = new Category { Nazwa = "Telefony komórkowe", ParentCategory = c5 };
            //    Category c511 = new Category { Nazwa = "Sony", ParentCategory = c51 };
            //        Category c5111 = new Category { Nazwa = "X-peria Z", ParentCategory = c511 };
            //        Category c5112 = new Category { Nazwa = "X-peria C", ParentCategory = c511 };
            //        Category c5113 = new Category { Nazwa = "X-peria P", ParentCategory = c511 };
            //    Category c512 = new Category { Nazwa = "Huawei", ParentCategory = c51 };
            //        Category c5121 = new Category { Nazwa = "P10", ParentCategory = c512 };
            //        Category c5122 = new Category { Nazwa = "G7", ParentCategory = c512 };
            //    Category c513 = new Category { Nazwa = "Alcatel", ParentCategory = c51 };
            //        Category c5131 = new Category { Nazwa = "One Touch", ParentCategory = c513 };
            //    Category c514 = new Category { Nazwa = "Samsung", ParentCategory = c51 };
            //        Category c5141 = new Category { Nazwa = "Galaxy", ParentCategory = c514 };
            //        Category c5142 = new Category { Nazwa = "Solid", ParentCategory = c514 };
            //        Category c5143 = new Category { Nazwa = "Monte", ParentCategory = c514};
            //        Category c5144 = new Category { Nazwa = "Avila", ParentCategory = c514 };
            //Category c52 = new Category { Nazwa = "Telewizory", ParentCategory = c5 };
            //    Category c521 = new Category { Nazwa = "LCD", ParentCategory = c52 };
            //    Category c522 = new Category { Nazwa = "LED", ParentCategory = c52 };
            //    Category c523 = new Category { Nazwa = "Kineskopowe", ParentCategory = c52 };
            //Category c53 = new Category { Nazwa = "Komputery", ParentCategory = c5 };
            //    Category c531 = new Category { Nazwa = "Stacjonarne", ParentCategory = c53 };
            //    Category c532 = new Category { Nazwa = "Przenoœne", ParentCategory = c53 };
            //    Category c533 = new Category { Nazwa = "Super Komputery", ParentCategory = c53 };
            //Category c54 = new Category { Nazwa = "Laptopy", ParentCategory = c5 };
            //    Category c541 = new Category { Nazwa = "Ultrabooki", ParentCategory = c54 };
            //    Category c542 = new Category { Nazwa = "Notebooki", ParentCategory = c54 };
            //    Category c543 = new Category { Nazwa = "Dotykowe", ParentCategory = c54 };

            ////podkategorie sport
            //Category c61 = new Category { Nazwa = "Pi³ki", ParentCategory = c6 };
            //Category c62 = new Category { Nazwa = "Siatki", ParentCategory = c6};
            //Category c63 = new Category { Nazwa = "Od¿ywki", ParentCategory = c6 };
            //    Category c631 = new Category { Nazwa = "Bia³ka", ParentCategory = c63 };
            //    Category c632 = new Category { Nazwa = "Wêglowodany", ParentCategory = c63 };
            //    Category c633 = new Category { Nazwa = "Kreatyny", ParentCategory = c63 };
            //Category c64 = new Category { Nazwa = "Sprzêt si³ownia", ParentCategory = c6 };

            ////podkategorie kultura
            //Category c71 = new Category { Nazwa = "Ksi¹¿ki", ParentCategory = c7 };
            //Category c72 = new Category { Nazwa = "RzeŸby", ParentCategory = c7 };

            ////podkategorie moda
            //Category c81 = new Category { Nazwa = "Skarpety", ParentCategory = c8 };
            //Category c82 = new Category { Nazwa = "Bluzki", ParentCategory = c8 };



            ////Atrybuty Motoryzacja
            //Models.Attribute at0 = new Models.Attribute { Nazwa = "Stan", Typ = "list", Category = c1 };

            ////Attrybuty pojazdy
            //Models.Attribute at1111 = new Models.Attribute { Nazwa = "Rodzaj paliwa", Typ = "list", Category = c11 };
            //Models.Attribute at111 = new Models.Attribute { Nazwa = "Kolor", Typ = "text", Category = c11 };
            //Models.Attribute at11 = new Models.Attribute { Nazwa = "Przebieg", Typ = "number", Category = c11 };
            //Models.Attribute at1 = new Models.Attribute { Nazwa = "Jednostka mocy", Typ="list", Category=c11};
            //Models.Attribute att = new Models.Attribute { Nazwa = "Moc Pojazdu", Typ = "number", Category = c11 };

            ////atrybuty traktory
            //Models.Attribute attt = new Models.Attribute { Nazwa = "Masa ci¹gnika[kg]", Typ = "number", Category = c112 };

            ////atrybuty motocykle
            //Models.Attribute atttt = new Models.Attribute { Nazwa = "Rodzaj napêdu", Typ = "list", Category = c12 };
            //Models.Attribute attq = new Models.Attribute { Nazwa = "Moc Pojazdu", Typ = "number", Category = c12 };

            ////Atrybuty Zwierzêta
            //Models.Attribute at2 = new Models.Attribute { Nazwa = "Wiek", Typ="number", Category=c4};
            //Models.Attribute at22 = new Models.Attribute { Nazwa = "Maœæ", Typ = "text", Category = c4 };

            ////Atrybuty Kwiaty
            //Models.Attribute at3 = new Models.Attribute { Nazwa = "Kolor Kwiatka", Typ = "text", Category=c21};
            //Models.Attribute at33 = new Models.Attribute { Nazwa = "Nazwa", Typ = "text", Category = c21 };

            ////atrybuty baseny
            //Models.Attribute at333 = new Models.Attribute { Nazwa = "Pojemnoœæ", Typ = "number", Category = c22 };

            ////atrybuty no¿e
            //Models.Attribute at3333 = new Models.Attribute { Nazwa = "D³ugoœæ no¿a", Typ = "number", Category = c313 };

            ////atrybut garnki
            //Models.Attribute at33333 = new Models.Attribute { Nazwa = "Œrednica", Typ = "double", Category = c33 };

            ////atrybuty tel.komórkowe
            //Models.Attribute aty = new Models.Attribute { Nazwa = "Przek¹tna ekranu", Typ = "double", Category = c51 };
            //Models.Attribute atyy = new Models.Attribute { Nazwa = "Aparat(px)", Typ = "double", Category = c51 };

            ////atrybuty telewizory
            //Models.Attribute atyyy = new Models.Attribute { Nazwa = "Przek¹tna ekranu", Typ = "double", Category = c52 };
            //Models.Attribute atyyyy = new Models.Attribute { Nazwa = "Marka", Typ = "list", Category = c52 };

            ////atrybuty komputery
            //Models.Attribute atyyyq = new Models.Attribute { Nazwa = "Iloœæ rdzeni", Typ = "number", Category = c53 };
            //Models.Attribute atyyyyq = new Models.Attribute { Nazwa = "Karta graficzna - Marka", Typ = "list", Category = c53 };

            ////atrybuty laptop
            //Models.Attribute atyyyyqq = new Models.Attribute { Nazwa = "Marka", Typ = "list", Category = c54 };

            ////atrybuty od¿ywki
            //Models.Attribute atyyyyqqq = new Models.Attribute { Nazwa = "Masa[kg]", Typ = "double", Category = c63 };

            ////atrybuty ksi¹¿ki
            //Models.Attribute atyyyyqqqq = new Models.Attribute { Nazwa = "Liczba stron", Typ = "number", Category = c71 };



            ////S³ownik marka laptop
            //Dictionary d111111 = new Dictionary { Pole = "Samsung", Attribute = atyyyyqq };
            //Dictionary d111112 = new Dictionary { Pole = "Asus", Attribute = atyyyyqq };
            //Dictionary d111113 = new Dictionary { Pole = "Acer", Attribute = atyyyyqq };
            //Dictionary d111114 = new Dictionary { Pole = "Inny", Attribute = atyyyyqq };


            ////S³ownik marka komputery
            //Dictionary d11111 = new Dictionary { Pole = "Nvidia", Attribute = atyyyyq };
            //Dictionary d11122 = new Dictionary { Pole = "AMD", Attribute = atyyyyq };
            //Dictionary d11133 = new Dictionary { Pole = "Inna", Attribute = atyyyyq };

            ////S³ownik marka telewizory
            //Dictionary d1111 = new Dictionary { Pole = "Samsung", Attribute = atyyyy };
            //Dictionary d1112 = new Dictionary { Pole = "LG", Attribute = atyyyy };
            //Dictionary d1113 = new Dictionary { Pole = "Sony", Attribute = atyyyy };
            //Dictionary d1114 = new Dictionary { Pole = "Inny", Attribute = atyyyy };


            ////S³ownik rodzaj napêdu
            //Dictionary d111 = new Dictionary { Pole = "Diesel", Attribute = atttt };
            //Dictionary d211 = new Dictionary { Pole = "Benzyna", Attribute = atttt };


            ////S³ownik rodzaj paliwa
            //Dictionary d11 = new Dictionary { Pole = "Diesel", Attribute = at1111 };
            //Dictionary d21 = new Dictionary { Pole = "Benzyna", Attribute = at1111 };
            //Dictionary d22 = new Dictionary { Pole = "Pr¹d", Attribute = at1111 };

            ////S³ownik dla Atrybutu at1 "Jednostka Mocy" - dropdownlist
            //Dictionary d1 = new Dictionary { Pole="KM", Attribute=at1};
            //Dictionary d2 = new Dictionary { Pole = "KW", Attribute = at1 };

            ////S³ownik dla Atrybutu at0 "Stan" - dla Morotyzacji - dropdownlist
            //Dictionary d3 = new Dictionary { Pole = "Nowy", Attribute = at0 };
            //Dictionary d4 = new Dictionary { Pole = "U¿ywany", Attribute = at0 };




            ////Volkswagen
            //Add a1 = new Add { User = u2, Content = "Sprzedam zadbanego VW, bez kabiny silnik chodzi elegancko polecam jeszcze zrobi z 10km", Data = DateTime.Now, Price = 5678.98, TelephoneNumber = "997887686", Title = "Sprzedam VW Golf", Category = c1112 };
            //AddAtrribute aa1 = new AddAtrribute { Add = a1, Attribute = at1, Value = "KM" };
            //AddAtrribute aa2 = new AddAtrribute { Add = a1, Attribute = att, Value = "120" };
            //AddAtrribute aa22 = new AddAtrribute { Add = a1, Attribute = at0, Value = "Nowy" };
            //AddAtrribute aa222 = new AddAtrribute { Add = a1, Attribute = at1111, Value = "Diesel" };
            //AddAtrribute aa2222 = new AddAtrribute { Add = a1, Attribute = at111, Value = "Czerwony" };
            //AddAtrribute aa22222 = new AddAtrribute { Add = a1, Attribute = at11, Value = "123000" };

            ////Seat
            //Add a11 = new Add { User = u4, Content = "Seat ma³o je¿d¿ony tylko do koœcio³a w niedziele", Data = DateTime.Now, Price = 6789.99, TelephoneNumber = "567765432", Title = "Seat Cordoba", Category = c1111 };
            //AddAtrribute aa11 = new AddAtrribute { Add = a11, Attribute = at1, Value = "KM" };
            //AddAtrribute aa21 = new AddAtrribute { Add = a11, Attribute = att, Value = "290" };
            //AddAtrribute aa221 = new AddAtrribute { Add = a11, Attribute = at0, Value = "U¿ywany" };
            //AddAtrribute aa2221 = new AddAtrribute { Add = a11, Attribute = at1111, Value = "Pr¹d" };
            //AddAtrribute aa22221 = new AddAtrribute { Add = a11, Attribute = at111, Value = "Zielony" };
            //AddAtrribute aa222221 = new AddAtrribute { Add = a11, Attribute = at11, Value = "13000" };


            ////kwiatek - dzikie
            //Add a2 = new Add { User = u3, Content = "Sprzedam kwiatek taki elegancki chwast", Data = DateTime.Now, Price = 4.99, TelephoneNumber = "564765443", Title = "Sprzedam kwatuszka", Category = c211 };
            //AddAtrribute aa3 = new AddAtrribute { Add = a2, Attribute = at3, Value = "Zielony" };
            //AddAtrribute aa33 = new AddAtrribute { Add = a2, Attribute = at33, Value = "Krokus" };


            ////Wladymirec
            //Add a3 = new Add { User = u4, Content = "Traktorek niewielki przebieg, orze jak ta lala", Data = DateTime.Now, Price = 2999.99, TelephoneNumber = "324324338", Title = "Traktor Wladymirec elegancki", Category = c1121 };
            //AddAtrribute aa4 = new AddAtrribute { Add = a3, Attribute = at1, Value = "KM" };
            //AddAtrribute aa5 = new AddAtrribute { Add = a3, Attribute = att, Value = "400" };
            //AddAtrribute aa55 = new AddAtrribute { Add = a3, Attribute = at0, Value = "U¿ywany" };
            //AddAtrribute aa555 = new AddAtrribute { Add = a3, Attribute = attt, Value = "2500" };
            //AddAtrribute aa00 = new AddAtrribute { Add = a3, Attribute = at1111, Value = "Diesel" };
            //AddAtrribute aa000 = new AddAtrribute { Add = a3, Attribute = at111, Value = "Czerwony" };
            //AddAtrribute aa0000 = new AddAtrribute { Add = a3, Attribute = at11, Value = "67000" };


            ////Ursus
            //Add a34 = new Add { User = u5, Content = "Perfekcyjny ursus polecam", Data = DateTime.Now, Price = 5999.99, TelephoneNumber = "324324338", Title = "Ursus C-330", Category = c1123 };
            //AddAtrribute aa44 = new AddAtrribute { Add = a34, Attribute = at1, Value = "KW" };
            //AddAtrribute aa54 = new AddAtrribute { Add = a34, Attribute = att, Value = "77" };
            //AddAtrribute aa554 = new AddAtrribute { Add = a34, Attribute = at0, Value = "U¿ywany" };
            //AddAtrribute aa5554 = new AddAtrribute { Add = a34, Attribute = attt, Value = "7000" };
            //AddAtrribute aa003 = new AddAtrribute { Add = a34, Attribute = at1111, Value = "Benzyna" };
            //AddAtrribute aa0004 = new AddAtrribute { Add = a34, Attribute = at111, Value = "Fiolet" };
            //AddAtrribute aa00003 = new AddAtrribute { Add = a34, Attribute = at11, Value = "79000" };


            ////Krajalnice
            //Add a4 = new Add { User = u4, Content = "Sprzedam krajalnice, kroji jak stara", Data = DateTime.Now, Price = 39.99, TelephoneNumber = "444444444", Title = "Krajalnica", Category = c311 };


            ////Zwierzeta - Domowe
            //Add a5 = new Add { User = u2, Content = "Sprzedam kota taniooo, bardzo elegancki polecam", Data = DateTime.Now, Price = 1.99, TelephoneNumber = "7777777778", Title = "Kot filemon - sprzedam", Category = c41 };
            //AddAtrribute aa6 = new AddAtrribute { Add = a5, Attribute = at2, Value = "39" };
            //AddAtrribute aa66 = new AddAtrribute { Add = a5, Attribute = at22, Value = "Kolorowa" };


            //BannedWord bw = new BannedWord { Word = "sedes" };
            //BannedWord bw1 = new BannedWord { Word = "piwo" };


            //context.BannedWords.AddOrUpdate(bw,bw1);
            //context.AddAttributes.AddOrUpdate(aa66,aa00003, aa0004, aa003, aa5554, aa554, aa54, aa44, aa555, aa00, aa000, aa0000,aa1, aa2,aa3,aa4,aa5,aa6,aa22, aa55, aa222, aa2222, aa22222, aa11, aa21, aa221, aa2221, aa22221, aa222221, aa33);
            //context.Dictionary.AddOrUpdate(d111111, d111112, d111113, d111114,d1, d2, d3, d4,d11,d21,d22, d111,d211, d1111, d1112, d1113, d1114, d11111, d11122, d11133);
            //context.Attributes.AddOrUpdate(atyyyyqqqq,atyyyyqqq, atyyyyqq, atyyyyq, atyyyq, atyyyy, atyyy, atyy, aty, at0, at1, at2, at3, att, at11, at111, at1111, at22,at33, attt, atttt, attq, at333, at3333, at33333);
            //context.Categories.AddOrUpdate(c631, c632, c633,c541, c542, c543,c531, c532, c533,c521, c522, c523,c5141, c5142, c5143, c5144,c5131, c5121, c5122,c5111, c5112, c5113, c511, c512,c513,c514,c1,c2,c3,c4,c5,c6,c7,c8,c11,c12,c13,c21,c22,c23,c24,c31,c32,c33,c34,c41,c51,c52,c53,c54,c61,c62,c63,c64,c71,c72,c81,c82,c311,c111,c112, c121,c122,c123,c131,c132,c133,c211,c212,c231,c232,c311,c312,c313,c341,c342, c1111,c1112, c1113,c1114, c1121, c1122, c1123);
            //context.Users.AddOrUpdate(u1, u2, u3, u4,u5,u6);
            //context.Adds.AddOrUpdate(a1, a2, a3, a4, a5, a11, a34);


        }

        



    }
}
