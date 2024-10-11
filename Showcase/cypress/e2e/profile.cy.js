describe('Profile test', () => {
    beforeEach(() => {
        cy.visit('http://localhost:8080/');
    });

    it('Check if CSS and JS files are loaded', () => {
        cy.get('link[href*="profile.css"]').should('exist');
        cy.get('link[href*="site.css"]').should('exist');
        cy.get('script[src*="site.js"]').should('exist');
    });

    it('Check if image is loaded correctly', () => {
        cy.get('img[src*="Geert-Jan.jpg"]').should('be.visible');
    });

    it('Check if personal data is displayed', () => {
        cy.contains('Geboortedatum: 14-04-2003').should('exist');
        cy.contains('Adres: Talmastraat 34, 3864DE').should('exist');
        cy.contains('E-mail: geertjanverkuil@gmail.com').should('exist');
        cy.contains('Mobiel: +31 6 45289517').should('exist');
    });

    it('Check if skills are displayed', () => {
        cy.contains('Oplossingsgericht').should('exist');
        cy.contains('Flexibel').should('exist');
        cy.contains('Communicatief vaardig').should('exist');
        cy.contains('Samenwerken').should('exist');
        cy.contains('Leergierig').should('exist');
    });
});
