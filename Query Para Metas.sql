select sum(f.NumAprenFicha)
from Meta m
inner join Oferta o on m.IdMeta = o.IdMetas
inner join Ficha f on o.IdOferta = f.IdOferta
where f.JornadaFicha in ('Diurna', 'Mixta', 'Nocturna')
and m.IdMeta = 1;


select count(f.NumAprenFicha) as CantCursos,
sum(f.NumAprenFicha) as ApPasan
from Ficha f
inner join Programa_Formacion p on f.IdPrograma = p.IdPrograma
inner join Oferta o on f.IdOferta = o.IdOferta
inner join Meta m on o.IdMetas = m.IdMeta
where f.JornadaFicha in ('Virtual')
  and m.IdMeta = 1
  and p.NivelPrograma = 'Tecnólogo'