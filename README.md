# Modificao aaaaa a PR

### Esteira
[![DesafioStone](https://github.com/KathlenGraziela/DesafioStone-Copa-Do-Mundo/actions/workflows/testPipeline.yml/badge.svg)](https://github.com/KathlenGraziela/DesafioStone-Copa-Do-Mundo/actions/workflows/testPipeline.yml)

## HEY, PRONTOS PARA O DESAFIO FINAL?

Em ano de Copa do Mundo, seu cliente está querendo fazer um sistema com objetivo de acompanhar o andamento dos clubes e os seus respectivos jogos.
Com isso devemos criar um sistema para que possamos fazer este acompanhamento.
O mesmo irá precisar de uma área administrativa para o cadastro dos itens essenciais e uma página de front-end para acompanhar todas as fases do campeonato.
	
## O sistema será desenvolvido em AspNet core MVC C#
- Para testes na API pode ser utilizado ferramentas como uma as opções abaixo:
- Site simples em javascript
- Shell script com curl
- postman
- Testes automatizados

# Funções administrativas na API:
- Cadastro de administradores 
- Cadastro de clube
- Cadastro de Jogos
- Cadastro de Fases do campeonato

## Estrutura:
- Administradores (Responsável pela geração do Token JWT (Opcional))
- Nome
- Email
- Senha (Criptografada opcional)
- Clube
- Nome
- Descrição
- URL Foto

## Jogo
- clube_a
- clube_b
- gols_clube _a
- gols_clube_b
- jogo_iniciado
- jogo_finalizado
- tempo_atual

## Fase do campeonato
- Nome
- Jogos da fase, exemplo:
- De grupos
- Oitavas
- Quarta
- Semi
- Final

## TECNOLOGIAS UTILIZADAS
- Dotnet MVC C# com API
- Testes com UnitTest (opcional)
- Banco de dados MySQL
- [Deploy] no próprio localhost usando docker
- Criar um front-end para utilizar a API criada, pode ser com Razor em c# ou HTML ou React, Angular ou Vue.js
- SUGESTÃO DE ORGANIZAÇÃO DO PROJETO
- Elaboração do kanban com definição dos entregáveis
- Elaboração do kanban (sugestão de utilização: Trello, Jira, etc) 
- Criação do backlog
- Detalhamento descritivo das tarefas da squad dentro dos seus cards (e não apenas com títulos genéricos no card)
- Formatação do kanban padrão "to do, doing, done"
- Definição de data de entrega das tarefas nos cards
- Definição de responsável pelo card ou checklist de completude
- Priorização dos cards (ex: tags com cores para maior relevância ou com títulos descritivos para nível de importância na priorização)

## CRITÉRIOS DE AVALIAÇÃO
- Itens mínimos para entrega
- Organização do projeto (Kanban no Trello, Jira ou outra ferramenta)

BackEnd:
- APIs construídas (segurança JWT opcional)
- Estrutura do projeto atendendo o requisito
- Apresentação das APIs no localhost
- CRUD's em funcionamento na área administrativa API
- Repositório do GIT para APIs e do Front-end

