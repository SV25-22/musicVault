using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MusicVault.Backend.Common;
    public static class EmailPatterns {
        public static string takenReviewBody = "Poštovani {ime} {prezime},\n\nObaveštavamo vas da vam je oduzeta recenzija {id} .\n";
        public static string newReviewBody = "Poštovani {ime} {prezime},\n\nObaveštavamo vas da vam je dodeljena nova recenzija {id}.\n";
        public static string publishedReviewBody = "Poštovani {ime} {prezime},\n\nObaveštavamo vas da je vaša recenzija {id} uspešno objavljena.\n";
        public static string registrationBody = "Poštovani {ime} {prezime},\n\nHvala vam što ste se registrovali. Molimo vas da potvrdite vašu registraciju klikom na sledeći link.\n";
        public static string loginBody = "Poštovani {ime} {prezime},\n\nPrimećen je pokušaj prijave na vaš nalog u trenutku {vreme}. Ako to niste bili Vi, molimo vas da promenite lozinku.\n";
        public static string reviewReviewBody = "Poštovani,\n\nObaveštavamo vas da vam je dodeljena recenzija {id} na overu.\n";
        public static string refuseReviewBody = "Poštovani {ime} {prezime},\n\nObaveštavamo vas da vam je odbijena recenzija {id} .\n Preporuka izmene: \n {poruka} \n";


        public static string takenReviewTitle = "Recenzija oduzeta {ime} {prezime}";
        public static string newReviewTitle = "Recenzija dodeljena {ime} {prezime}";
        public static string publishedReviewTitle = "Recenzija objavljena {ime} {prezime}";
        public static string reviewReviewTitle = "Recenzija na overi";
        public static string refuseReviewTitle = "Recenzija odbijena {ime} {prezime}";
        public static string registrationTitle = "Potvrda registracije {ime} {prezime}";
        public static string loginTitle = "Pokusaj prijave {ime} {prezime}";

        public static string InsertPatternParamaters(string template, string ime, string prezime) {
            return template.Replace("{ime}", ime).Replace("{prezime}", prezime);
        }
    }
