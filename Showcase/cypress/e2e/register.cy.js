describe('Account Aanmaken Tests', () => {
    beforeEach(() => {
        cy.visit('http://localhost:8080/Identity/Account/Register');

        cy.get('button[data-cookie-string]').click();
    });

    it('Create new account', () => {
        cy.get('input[autocomplete="firstname"]').type('John');
        cy.get('input[autocomplete="lastname"]').type('Doe');
        cy.get('input[autocomplete="username"]').type('johndoe@example.commm');
        cy.get('input[autocomplete="nickname"]').type('johndoe');
        cy.get('input[autocomplete="new-password"]').eq(0).type('Password123!password');
        cy.get('input[autocomplete="new-password"]').eq(1).type('Password123!password');

        cy.get('#registerSubmit').click();

        cy.get('#confirm-link').click();
        cy.url().should('include', '/ConfirmEmail');
    });
});