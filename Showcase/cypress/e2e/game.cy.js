describe('Login', () => {
    before(() => {
        cy.session('admin', () => {
            cy.visit('http://localhost:8080/Identity/Account/Login');

            cy.get('button[data-cookie-string]').click();
            cy.get('input[name="Input.Email"]').type('admin@admin.nl');
            cy.get('input[name="Input.Password"]').type('Admin123!');

            cy.get('#login-submit').click();

            cy.get('#login').should('not.exist');
        });
    });

    it('Should create a new game and check if gameboard', () => {
        cy.visit('https://localhost:32772/api/game');

        cy.get('#createGameButton').click();

        cy.get('#gameBoard')
            .should('be.visible');
    });
});