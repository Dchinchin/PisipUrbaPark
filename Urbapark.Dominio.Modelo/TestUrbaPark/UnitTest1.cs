using Microsoft.EntityFrameworkCore;
using UrbaPark.Aplicacion.Servicio;
using UrbaPark.Aplicacion.ServicioImpl;
using UrbaPark.Infraestructura.AccesoDatos;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TestUrbaPark
{
    public class Tests
    {
        private IBitacoraServicio _bitacoraSevicio;
        private Pisip_UrbanParkContext _pisip_UrbanParkContext;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Pisip_UrbanParkContext>()
                .UseSqlServer("Data Source=DESKTOP-M6S1CEH\\SQLEXPRESS;Initial Catalog=Pisip_UrbanPark;Integrated Security=True;Encrypt=True; TrustServerCertificate=True")
                .Options;
            _pisip_UrbanParkContext = new Pisip_UrbanParkContext(options);
            _bitacoraSevicio = new BitacoraServicioImpl(_pisip_UrbanParkContext);
        }

        [Test]
        public async Task Test1()
        {
            var bitacora = new Bitacora
            {
                id_bitacora = 1,
                descripcion = "Test Description",
                fecha_hora = DateTime.Now,
                imagen_url = "http://example.com/image.jpg",
                id_cronogramas = 1
            };
            await _bitacoraSevicio.BitacoraAddAsync(bitacora);
            await _bitacoraSevicio.BitacoraGetAllAsync();
            Assert.Pass();
        }
        [TearDown]
        public void TearDown()
        {
            _pisip_UrbanParkContext.Dispose();
        }
    }
}