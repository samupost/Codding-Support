# Cógigo para mover arquivos (delimitados a partir de uma lista) de uma pasta pra outra ------------------------------------------------------------------------------
import pandas as pd
import os

# Determina o caminho do arquivo delimitador
caminho_csv = r'C:\caminho'
# Le o df com os nomes dos arquivos e propoe o delimitador como ';'
df_raw = pd.read_csv(caminho_csv, delimiter=';')
# Seleciona só os registros de interesse por meio de filtro na coluna de flag
df = df_raw[df_raw['transferir'] == 1]
# Define os caminhos das pastas
pasta_origem = r'\\BRAFPS01\Share\N1\Logistica\07 anos\PLANEJAMENTO\Sam\Projetos\Transferencia de arquivos transporte\ORIGEM'
pasta_destino = r'\\BRAFPS01\Share\N1\Logistica\07 anos\PLANEJAMENTO\Sam\Projetos\Transferencia de arquivos transporte\DESTINO'

# Itera sobre cada linha do df
for index, row in df.iterrows():
    nome_arquivo = row['nome_arquivos']  # Coluna com os nomes dos arquivos
    caminho_completo = os.path.join(pasta_origem, nome_arquivo)

    # Verificando se o arquivo existe
    if os.path.exists(caminho_completo):
        # Movendo o arquivo
        novo_caminho = os.path.join(pasta_destino, nome_arquivo)
        os.rename(caminho_completo, novo_caminho)
        print(f"Arquivo {nome_arquivo} movido com sucesso.")
    else:
        print(f"Arquivo {nome_arquivo} não encontrado.")
