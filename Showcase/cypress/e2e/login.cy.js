describe('Login Test', () => {
    it('Should log in with admin credentials and check if logged in', () => {
        cy.visit('https://localhost:32772/Identity/Account/Login');

        cy.get('button[data-cookie-string]').click();

        cy.get('input[name="Input.Email"]').type('admin@admin.nl');
        cy.get('input[name="Input.Password"]').type('Admin123!');

        cy.get('#login-submit').click();

        cy.get('#login').should('not.exist');
    });
});
