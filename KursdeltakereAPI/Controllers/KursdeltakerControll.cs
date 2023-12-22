using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using kursdeltakereAPI.Modeller;

namespace kursdeltakereAPI.Controllers
{
    public class KursdeltakerControll : Controller
    {

        private readonly IMongoCollection<Kursdeltaker> _kursdeltakerCollection;

        public KursdeltakerControll(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("KursCluster");
            _kursdeltakerCollection = database.GetCollection<Kursdeltaker>("Kursdeltakere");
        }


        // GET:  - Håndterer forespørsler om å vise en liste over deltakere
        public ActionResult Index()
        {
            var kursdeltakere = _kursdeltakerCollection.Find(d => true).ToList();
            return View(kursdeltakere); 
        }

        // GET: - Håndterer forespørsler for å vise detaljer om en spesifikk deltaker
        public ActionResult Details(int id)
        {
            var deltaker = _kursdeltakerCollection.Find(d => d.Id == id).FirstOrDefault();
            return View(deltaker);
        }

        // GET: - Håndterer GET-forespørsler for å vise skjemaet for å opprette en ny deltaker
        public ActionResult Create()
        {
            return View();
        }

        // POST:  - Håndterer POST-forespørsler nå jeg sender inn skjemaet for å redigsere info
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kursdeltaker kursdeltaker)
        {
            try
            {
                _kursdeltakerCollection.InsertOne(kursdeltaker);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: - GET-forespørsler for å vise skjemat for å redigere info
        public ActionResult Edit(int id)
        {
            var deltaker = _kursdeltakerCollection.Find(d => d.Id == id).FirstOrDefault();
            return View(deltaker);
        }

        // POST: KursdeltakerControll/Edit/5 - 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Kursdeltaker kursdeltaker)
        {
            try
            {
                _kursdeltakerCollection.ReplaceOne(d => d.Id == id, kursdeltaker); //Må huske å endre ReplaceOne metoden til mine behov senere
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: KursdeltakerControll/Delete/5
        public ActionResult Delete(int id)
        {
            var deltaker = _kursdeltakerCollection.Find(d => d.Id  == id ).FirstOrDefault();
            return View(deltaker);
        }

        // POST: KursdeltakerControll/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Kursdeltaker kursdeltaker)
        {
            try
            {
                _kursdeltakerCollection.DeleteOne(d => d.Id == id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
