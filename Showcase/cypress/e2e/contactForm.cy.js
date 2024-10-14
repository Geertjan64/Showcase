describe('Contactform test', () => {
    beforeEach(() => {
        cy.visit('http://localhost:8080/Contact/ContactForm');
        cy.get('button[data-cookie-string]').click();
        cy.intercept('POST', '/recaptcha/api/siteverify', {
            success: true
        });
    });

    it('Should fill out and submit the contact form', () => {
        cy.get('input[name="FirstName"]').type('John');
        cy.get('input[name="LastName"]').type('Doe');
        cy.get('input[name="Subject"]').type('Vraag over product');
        cy.get('input[name="FromEmail"]').type('johndoe@example.com');
        cy.get('input[name="Mobile"]').type('0645289517');
        cy.get('textarea[name="Body"]').type('Hallo, ik wil graag contact met u! Kunt u mij bellen? :).');

        cy.get('button[type="submit"]').click();

        cy.wait(10000);

        cy.get('.alert-success').should('be.visible');
        cy.get('.alert-success').contains('Het bericht is succesvol verzonden!');
    });
});
