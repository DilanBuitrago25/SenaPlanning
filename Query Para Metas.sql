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
where f.JornadaFicha in ('Diurna', 'Mixta', 'Nocturna')
and m.IdMeta = 1
and p.NivelPrograma = 'Tecnólogo'

select * 
from Ficha, Programa_Formacion
where ficha.IdPrograma = Programa_Formacion.IdPrograma and
Programa_Formacion.NivelPrograma = 'Tecnólogo'

SELECT f.*
FROM Ficha f
LEFT JOIN Programa_Formacion p ON f.IdPrograma = p.IdPrograma
LEFT JOIN Oferta o ON f.IdOferta = o.IdOferta
LEFT JOIN Meta m ON o.IdMetas = m.IdMeta
WHERE f.JornadaFicha IN ('Diurna', 'Mixta', 'Nocturna')
AND m.IdMeta = 1
AND p.NivelPrograma = 'Tecnólogo'