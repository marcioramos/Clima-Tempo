
DECLARE @min date = (Select min(DataPrevisao) from PrevisaoClima);
DECLARE @max date = (Select max(DataPrevisao) from PrevisaoClima);
DECLARE @diff int = DATEDIFF(DAY, @min, @max);
Select @min, @max, @diff, DATEADD(DAY , @diff + 1, @min)


INSERT INTO PrevisaoClima(CidadeId, DataPrevisao,                           Clima, TemperaturaMinima, TemperaturaMaxima)
                   SELECT CidadeId, DATEADD(DAY , @diff + 1, DataPrevisao), Clima, TemperaturaMinima, TemperaturaMaxima FROM PrevisaoClima;

