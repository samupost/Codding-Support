' Sub para enviar email (anexo, data no assunto, imagem e site no body) ------------------------------------------------------------------------------------------------------------

Sub EnviarEmail()

    Windows("myWorkbook").Activate
    Sheets("sheet1").Select
    DataRel = Sheets("sheet1").Cells(1, 1).Value

    ' Converte a string para o formato de data. Caso dê erro o código segue (On Error Resume Next), mas o comportamento padrao de erros é retomado logo em seguida (On Error GoTo 0).
    On Error Resume Next
    DataComoData = CDate(DataRel)
    On Error GoTo 0

    ' Valida a data e extrai dia, mes e ano. Em caso de falso, exibe uma mensagem.
    If IsDate(DataComoData) Then
        Dia = Day(DataComoData)
        Mes = Month(DataComoData)
        Ano = Year(DataComoData)
    Else
        MsgBox "A célula não contém uma data válida."
    End If
  
    AnexoName = "Relatorio" & Dia & "-" & Mes & "-" & Ano & ".xlsx"
    AnexoCaminho = "caminho do anexo"
    Anexo = AnexoCaminho & AnexoName
    site = "<a href=""url"">clicando aqui</a>" ' Esse código HTML propoe o site como string clicável e a mascara (como o link será exibido) é o texto "clicando aqui".
    
    ' Concatena e stringa os emails.
    Dim result As String
    Dim sepr As String
    Dim x As String
    sepr = ";"
    ultLinha = Sheets("sheetContatos").Range("A1048576").End(xlUp).Row
    Sheets("sheetContatos").Select
    For i = 1 To ultLinha
    		x = Cells(i, 1).Value
    		result = result & x & sepr
    Next
    result = Left(result, Len(result) - Len(sepr))
    MailList = result

    ' Seleciona e copia parte da planilha para colar no corpo do email como imagem.
    Sheets("sheet1").Range("A1:M40").Copy

    ' Invoca o outlook e cria o email.
    Set objOutlook = CreateObject("Outlook.Application")
    Set Email = objOutlook.CreateItem(0)
    ' Constroi o email com os parametros desejados.
    Email.Display
    Email.To = MailList
    Email.cc = "endereco1; endereco2"
    Email.Subject = "Relatorio - " & DataRel
    Email.htmlbody = "<br><br>Para mais informações acesse " & site & Email.htmlbody ' Adiciona o link e a assinatura.
    Email.GetInspector.WordEditor.Range.PasteSpecial Link:=False, DataType:=wdPasteHTML, Placement:=wdInLine, DisplayAsIcon:=False ' Invoca o inspetor para colar a imagem copiada.
    Email.Attachments.Add (Anexo)
    ' Email.Send -- Ao descomentar esse código o e-mail é enviado automaticamente. A menos que seja a proposta, mantenha-o comentado para enviar o email manualmente após conferencia.

End Sub
