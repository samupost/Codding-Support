// Inserir linha em branco ------------------------------------------------------------------------------------------------

AddLinhaBranco = Table.InsertRows(#"Linhas Filtradas", 0,{Record.FromList(List.Repeat({""},Table.ColumnCount(#"Linhas Filtradas")),Table.ColumnNames(#"Linhas Filtradas"))})
    
// Coluna calculada para a diferença em dias úteis ------------------------------------------------------------------------

AdicionarDiasUteis = Table.AddColumn(Source, "DiasUteis", each 
    let
        StartDate = [DataInicio],
        EndDate = [DataFim],
        // Gera uma lista de todas as datas entre as duas datas
        TodasAsDatas = List.Dates(StartDate, Duration.Days(EndDate - StartDate) + 1, #duration(1, 0, 0, 0)),
        // Filtra apenas os dias úteis (segunda a sexta)
        DiasUteis = List.Select(TodasAsDatas, each Date.DayOfWeek(_, Day.Monday) < 5)
    in
        List.Count(DiasUteis)
)

// Extrai só numeros de uma string com texto -----------------------------------------------------------------------------

ExtrairNumeros= Table.AddColumn(dProjeto_Table, "Vlr BRL", each Text.Select([Txt BRL],{"0".."9", "."}))
