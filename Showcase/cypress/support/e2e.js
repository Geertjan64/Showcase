// ***********************************************************
// This example support/e2e.js is processed and
// loaded automatically before your test files.
//
// This is a great place to put global configuration and
// behavior that modifies Cypress.
//
// You can change the location of this file or turn off
// automatically serving support files with the
// 'supportFile' configuration option.
//
// You can read more here:
// https://on.cypress.io/configuration
// ***********************************************************

// Import commands.js using ES2015 syntax:
import './commands'

// Alternatively you can use CommonJS syntax:
// require('./commands')

Cypress.on('uncaught:exception', (err, runnable) => {
    // Controleer op de specifieke error message van Google reCAPTCHA
    if (err.message.includes('Invalid site key or not loaded in api.js')) {
        // Cypress negeert deze fout en laat de test doorgaan
        return false;
    }

    // Laat andere fouten wel door
    return true;
});