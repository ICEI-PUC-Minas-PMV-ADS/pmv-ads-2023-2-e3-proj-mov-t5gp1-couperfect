# Metodologia

O desenvolvimento do software será realizado na plataforma GitHub. Nela, serão registradas as tarefas (issues) para cada requisito ou bug encontrado na aplicação. Para cada tarefa, será criada uma branch do código, que passará pelo processo de avaliação em um Pull Request (PR). Isso permitirá avaliar a qualidade do código e discutir se a solução proposta realmente atende ao requisito que a tarefa busca resolver. Dessa forma, é garantido um processo de desenvolvimento colaborativo e transparente.

## Relação de Ambientes de Trabalho

Os artefatos do projeto são desenvolvidos a partir de diversas plataformas e a relação dos ambientes com seu respectivo propósito deverá ser apresentada em uma tabela que especifica que detalha Ambiente, Plataforma e Link de Acesso. 
Nota: Vide documento modelo do estudo de caso "Portal de Notícias" e defina também os ambientes e frameworks que serão utilizados no desenvolvimento de aplicações móveis.

## Controle de Versão

A ferramenta de controle de versão adotada no projeto foi o
[Git](https://git-scm.com/), sendo que o [Github](https://github.com)
foi utilizado para hospedagem do repositório.

O projeto segue a seguinte convenção para o nome de branches:

- `main` : mais recente versão estável e testada do software.
- `dev` : mais recente versão estável e com recursos completos mais recente.
- `feat/(nome do issue)` : versão instável para trabalho numa feature, deve ser mergeada na dev após teste, via Pull Request.
- `work/(nome do issue)/(nome do desenvolvedor)` : versão instável para trabalho numa feature por um desenvolvedor em especifico, deve ser mergeada na feat/(nome da isssue) após teste, via Pull Request.
- `release/(etapa 1, etapa 2, .., etapa 6)` : copia da versão mais recente da main no momento da entrega da etapa.

Quanto à gerência de issues, o projeto adota a seguinte convenção para
etiquetas:

- `doc`: Criar, melhorar ou corrigir documentação
- `bug`: Correção de erro ou falha
- `feat`: Novas features ou melhorias
- `question` : Duvida, levantamento ou descoberta

## Gerenciamento de Projeto

### Divisão de Papéis

 - Scrum Master: Luiz Eduardo de Jesus Santana
 - Product Owner: Luiz Eduardo de Jesus Santana
 - Designer: Adeilton Rodrigues Farias Junior
 - Equipe de Desenvolvimento
    - Adeilton Rodrigues Farias Junior
    - Carlos Alberto Mendonça Vasconcelos
    - Gustavo Henrique De Jesus Almeida
    - Luiz Eduardo de Jesus Santana 
    - Pedro Rafael da Cruz Almeida

### Processo

Para a divisão de tarefas em projetos de desenvolvimento de software, é utilizado as "issues" do Github, já que esse recurso se integra facilmente ao fluxo de trabalho do Git que é seguido pela aplicação. Cada issue criada no Github é associada a uma branch específica, o que possibilita que, ao mesclar (merge) essa branch em outra, a issue seja concluída automaticamente. Essa abordagem simplifica e agiliza o processo de gerenciamento de tarefas e facilita a colaboração entre membros da equipe.

### Ferramentas

As ferramentas empregadas no projeto são:

- Editor de código.
- Ferramentas de comunicação
- Ferramentas de desenho de tela (_wireframing_)

O editor de código foi escolhido porque ele possui uma integração com o sistema de versão. As ferramentas de comunicação utilizadas possuem integração semelhante e por isso foram selecionadas. Por fim, para criar diagramas utilizamos essa ferramenta por melhor captar as necessidades da nossa solução.

Liste quais ferramentas foram empregadas no desenvolvimento do projeto, justificando a escolha delas, sempre que possível.
 
> **Possíveis Ferramentas que auxiliarão no gerenciamento**: 
> - [Slack](https://slack.com/)
> - [Github](https://github.com/)
